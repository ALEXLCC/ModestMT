

phantom.outputEncoding = 'gbk';

var system = require('system');

var URL;
var PATH;

if (system.args.length != 3) {
	console.log('Please use like: command [URL] [Path]');
	phantom.exit();
} else {
	URL = system.args[1];
	PATH = system.args[2];
}

var fs = require('fs');

var page = require('webpage').create();
page.viewportSize = { width: 1024, height: 8000 };
page.loadImages = false;

page.open(URL, function (status) {
	// Check for page load success
	if (status !== "success") {
		console.log("Unable to access network");
	} else {
		console.log('Load ' + URL + ' finished.');

		var titleContent = page.evaluate(function () {
			var tagname = document.getElementsByTagName('li');  //获取指定元素

			for (var i = 0; i < tagname.length; i++) {     //遍历获得的元素
				if (tagname[i].className == 'pagination') {     //如果获得的元素中的class的值等于指定的类名，就赋值给tagnameAll
					return tagname[i].outerHTML;
				}
			}
			phantom.exit();
		});

		fs.write(PATH + '.html', titleContent, 'w');

		console.log('Write ' + URL + ' to local end.');
	}
	phantom.exit();
});

