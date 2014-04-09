phantom.outputEncoding = 'gbk';

var system = require('system');

var URL;
var PATH;
var TIMEOUT;

if (system.args.length != 4) {
	console.log('Please use like: command [URL] [Path] [Timeout]');
	phantom.exit();
} else {
	URL = system.args[1];
	PATH = system.args[2];
	TIMEOUT = system.args[3];
}

var fs = require('fs');

var page = require('webpage').create();
page.viewportSize = { width: 1024, height: 8000 };
page.loadImages = false;

page.open(URL, function (status) {
	// Check for page load success
	if (status !== "success") {
		console.log("Unable to access network");
		phantom.exit();
	} else {
		console.log('Start loading  ' + URL);
		waitFor(function () {
			// Check in the page if a specific element is now visible
			return page.evaluate(function () {
				return document.getElementById('desc-lazyload-container').innerHTML != '加载中...';
			});
		}, function () {
			console.log('Load ' + URL + ' finished.');

			var titleContent = page.evaluate(function () {
				return document.getElementById('mod-detail-hd').outerHTML;
			});

			var dtTabContent = page.evaluate(function () {
				return document.getElementById('dt-tab').outerHTML;
			});

			var detailContent = page.evaluate(function () {
				return document.getElementById('desc-lazyload-container').outerHTML;
			});

			fs.write(PATH + '.html', titleContent, 'w');

			fs.write(PATH + '.html', dtTabContent, 'a');

			fs.write(PATH + '.html', detailContent, 'a');

			console.log('Write ' + URL + ' to local end.');
			phantom.exit();
		}, TIMEOUT);
	}
});

/**
 * Wait until the test condition is true or a timeout occurs. Useful for waiting
 * on a server response or for a ui change (fadeIn, etc.) to occur.
 *
 * @param testFx javascript condition that evaluates to a boolean,
 * it can be passed in as a string (e.g.: "1 == 1" or "$('#bar').is(':visible')" or
 * as a callback function.
 * @param onReady what to do when testFx condition is fulfilled,
 * it can be passed in as a string (e.g.: "1 == 1" or "$('#bar').is(':visible')" or
 * as a callback function.
 * @param timeOutMillis the max amount of time to wait. If not specified, 3 sec is used.
 */
function waitFor(testFx, onReady, timeOutMillis) {
	console.log(timeOutMillis);
	var maxtimeOutMillis = timeOutMillis ? timeOutMillis : 60000, //< Default Max Timout is 3s
        start = new Date().getTime(),
        condition = false,
        interval = setInterval(function () {
        	if ((new Date().getTime() - start < maxtimeOutMillis) && !condition) {
        		// If not time-out yet and condition not yet fulfilled
        		condition = (typeof (testFx) === "string" ? eval(testFx) : testFx()); //< defensive code
        	} else {
        		if (!condition) {
        			// If condition still not fulfilled (timeout but condition is 'false')
        			console.log("'waitFor()' timeout");
        			phantom.exit();
        		} else {
        			typeof (onReady) === "string" ? eval(onReady) : onReady(); //< Do what it's supposed to do once the condition is fulfilled
        			clearInterval(interval); //< Stop this interval
        		}
        	}
        }, 1000); //< repeat check every 250ms
};