/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 140);
/******/ })
/************************************************************************/
/******/ ({

/***/ 1:
/***/ (function(module, exports) {

eval("/*\n\tMIT License http://www.opensource.org/licenses/mit-license.php\n\tAuthor Tobias Koppers @sokra\n*/\nmodule.exports = function(src) {\n\tfunction log(error) {\n\t\t(typeof console !== \"undefined\")\n\t\t&& (console.error || console.log)(\"[Script Loader]\", error);\n\t}\n\n\t// Check for IE =< 8\n\tfunction isIE() {\n\t\treturn typeof attachEvent !== \"undefined\" && typeof addEventListener === \"undefined\";\n\t}\n\n\ttry {\n\t\tif (typeof execScript !== \"undefined\" && isIE()) {\n\t\t\texecScript(src);\n\t\t} else if (typeof eval !== \"undefined\") {\n\t\t\teval.call(null, src);\n\t\t} else {\n\t\t\tlog(\"EvalError: No eval function available\");\n\t\t}\n\t} catch (error) {\n\t\tlog(error);\n\t}\n}\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi4vbm9kZV9tb2R1bGVzL3NjcmlwdC1sb2FkZXIvYWRkU2NyaXB0LmpzP2E2ODkiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQUVBO0FBQ0E7QUFDQTtBQUNBOztBQUVBO0FBQ0E7QUFDQTtBQUNBLEdBQUc7QUFDSDtBQUNBLEdBQUc7QUFDSDtBQUNBO0FBQ0EsRUFBRTtBQUNGO0FBQ0E7QUFDQSIsImZpbGUiOiIxLmpzIiwic291cmNlc0NvbnRlbnQiOlsiLypcblx0TUlUIExpY2Vuc2UgaHR0cDovL3d3dy5vcGVuc291cmNlLm9yZy9saWNlbnNlcy9taXQtbGljZW5zZS5waHBcblx0QXV0aG9yIFRvYmlhcyBLb3BwZXJzIEBzb2tyYVxuKi9cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24oc3JjKSB7XG5cdGZ1bmN0aW9uIGxvZyhlcnJvcikge1xuXHRcdCh0eXBlb2YgY29uc29sZSAhPT0gXCJ1bmRlZmluZWRcIilcblx0XHQmJiAoY29uc29sZS5lcnJvciB8fCBjb25zb2xlLmxvZykoXCJbU2NyaXB0IExvYWRlcl1cIiwgZXJyb3IpO1xuXHR9XG5cblx0Ly8gQ2hlY2sgZm9yIElFID08IDhcblx0ZnVuY3Rpb24gaXNJRSgpIHtcblx0XHRyZXR1cm4gdHlwZW9mIGF0dGFjaEV2ZW50ICE9PSBcInVuZGVmaW5lZFwiICYmIHR5cGVvZiBhZGRFdmVudExpc3RlbmVyID09PSBcInVuZGVmaW5lZFwiO1xuXHR9XG5cblx0dHJ5IHtcblx0XHRpZiAodHlwZW9mIGV4ZWNTY3JpcHQgIT09IFwidW5kZWZpbmVkXCIgJiYgaXNJRSgpKSB7XG5cdFx0XHRleGVjU2NyaXB0KHNyYyk7XG5cdFx0fSBlbHNlIGlmICh0eXBlb2YgZXZhbCAhPT0gXCJ1bmRlZmluZWRcIikge1xuXHRcdFx0ZXZhbC5jYWxsKG51bGwsIHNyYyk7XG5cdFx0fSBlbHNlIHtcblx0XHRcdGxvZyhcIkV2YWxFcnJvcjogTm8gZXZhbCBmdW5jdGlvbiBhdmFpbGFibGVcIik7XG5cdFx0fVxuXHR9IGNhdGNoIChlcnJvcikge1xuXHRcdGxvZyhlcnJvcik7XG5cdH1cbn1cblxuXG5cbi8vLy8vLy8vLy8vLy8vLy8vL1xuLy8gV0VCUEFDSyBGT09URVJcbi8vIC4uL25vZGVfbW9kdWxlcy9zY3JpcHQtbG9hZGVyL2FkZFNjcmlwdC5qc1xuLy8gbW9kdWxlIGlkID0gMVxuLy8gbW9kdWxlIGNodW5rcyA9IDEgMiAzIDQiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///1\n");

/***/ }),

/***/ 140:
/***/ (function(module, exports, __webpack_require__) {

eval("__webpack_require__(1)(__webpack_require__(141))//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9qcy9jb250cm9sbGVycy9leGhpYml0RGV0YWlsc0NvbnRyb2xsZXIuZXhlYy5qcz81ZjQ0Il0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBIiwiZmlsZSI6IjE0MC5qcyIsInNvdXJjZXNDb250ZW50IjpbInJlcXVpcmUoXCIhIS9ob21lL21pY2hhZWwvRG90TmV0UHJvamVjdHMvUGhvdG9FeGhpYml0ZXIvc3JjL25vZGVfbW9kdWxlcy9zY3JpcHQtbG9hZGVyL2FkZFNjcmlwdC5qc1wiKShyZXF1aXJlKFwiISEvaG9tZS9taWNoYWVsL0RvdE5ldFByb2plY3RzL1Bob3RvRXhoaWJpdGVyL3NyYy9ub2RlX21vZHVsZXMvcmF3LWxvYWRlci9pbmRleC5qcyEvaG9tZS9taWNoYWVsL0RvdE5ldFByb2plY3RzL1Bob3RvRXhoaWJpdGVyL3NyYy9DbGllbnQvanMvY29udHJvbGxlcnMvZXhoaWJpdERldGFpbHNDb250cm9sbGVyLmV4ZWMuanNcIikpXG5cblxuLy8vLy8vLy8vLy8vLy8vLy8vXG4vLyBXRUJQQUNLIEZPT1RFUlxuLy8gLi9qcy9jb250cm9sbGVycy9leGhpYml0RGV0YWlsc0NvbnRyb2xsZXIuZXhlYy5qc1xuLy8gbW9kdWxlIGlkID0gMTQwXG4vLyBtb2R1bGUgY2h1bmtzID0gNCJdLCJzb3VyY2VSb290IjoiIn0=\n//# sourceURL=webpack-internal:///140\n");

/***/ }),

/***/ 141:
/***/ (function(module, exports) {

eval("module.exports = \"var ExhibitDetailsController = function (followingService) {\\n    var followButton;\\n\\n    var init = function () {\\n        $(\\\".js-toggle-follow\\\").click(toggleFollowing);\\n    };\\n\\n    var toggleFollowing = function (e) {\\n        followButton = $(e.target);\\n        var followeeId = followButton.attr(\\\"data-user-id\\\");\\n\\n        if (followButton.hasClass(\\\"btn-default\\\"))\\n            followingService.createFollowing(followeeId, done, fail);\\n        else\\n            followingService.deleteFollowing(followeeId, done, fail);\\n    };\\n\\n    var done = function () {\\n        var text = (followButton.text() == \\\"Follow\\\") ? \\\"Following\\\" : \\\"Follow\\\";\\n        followButton.toggleClass(\\\"btn-info\\\").toggleClass(\\\"btn-default\\\").text(text);\\n    };\\n\\n    var fail = function () {\\n        alert(\\\"Something failed\\\");\\n    };\\n\\n    return {\\n        init: init\\n    }\\n\\n}(FollowingService);\\n\"//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9qcy9jb250cm9sbGVycy9leGhpYml0RGV0YWlsc0NvbnRyb2xsZXIuZXhlYy5qcz83Mzc3Il0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBLDhFQUE4RSx1QkFBdUIsZ0NBQWdDLDBEQUEwRCxRQUFRLDRDQUE0QyxxQ0FBcUMsK0RBQStELDhIQUE4SCxxRkFBcUYsUUFBUSxnQ0FBZ0Msc0ZBQXNGLHlGQUF5RixRQUFRLGdDQUFnQyxzQ0FBc0MsUUFBUSxnQkFBZ0IsMkJBQTJCLEtBQUssbUJBQW1CIiwiZmlsZSI6IjE0MS5qcyIsInNvdXJjZXNDb250ZW50IjpbIm1vZHVsZS5leHBvcnRzID0gXCJ2YXIgRXhoaWJpdERldGFpbHNDb250cm9sbGVyID0gZnVuY3Rpb24gKGZvbGxvd2luZ1NlcnZpY2UpIHtcXG4gICAgdmFyIGZvbGxvd0J1dHRvbjtcXG5cXG4gICAgdmFyIGluaXQgPSBmdW5jdGlvbiAoKSB7XFxuICAgICAgICAkKFxcXCIuanMtdG9nZ2xlLWZvbGxvd1xcXCIpLmNsaWNrKHRvZ2dsZUZvbGxvd2luZyk7XFxuICAgIH07XFxuXFxuICAgIHZhciB0b2dnbGVGb2xsb3dpbmcgPSBmdW5jdGlvbiAoZSkge1xcbiAgICAgICAgZm9sbG93QnV0dG9uID0gJChlLnRhcmdldCk7XFxuICAgICAgICB2YXIgZm9sbG93ZWVJZCA9IGZvbGxvd0J1dHRvbi5hdHRyKFxcXCJkYXRhLXVzZXItaWRcXFwiKTtcXG5cXG4gICAgICAgIGlmIChmb2xsb3dCdXR0b24uaGFzQ2xhc3MoXFxcImJ0bi1kZWZhdWx0XFxcIikpXFxuICAgICAgICAgICAgZm9sbG93aW5nU2VydmljZS5jcmVhdGVGb2xsb3dpbmcoZm9sbG93ZWVJZCwgZG9uZSwgZmFpbCk7XFxuICAgICAgICBlbHNlXFxuICAgICAgICAgICAgZm9sbG93aW5nU2VydmljZS5kZWxldGVGb2xsb3dpbmcoZm9sbG93ZWVJZCwgZG9uZSwgZmFpbCk7XFxuICAgIH07XFxuXFxuICAgIHZhciBkb25lID0gZnVuY3Rpb24gKCkge1xcbiAgICAgICAgdmFyIHRleHQgPSAoZm9sbG93QnV0dG9uLnRleHQoKSA9PSBcXFwiRm9sbG93XFxcIikgPyBcXFwiRm9sbG93aW5nXFxcIiA6IFxcXCJGb2xsb3dcXFwiO1xcbiAgICAgICAgZm9sbG93QnV0dG9uLnRvZ2dsZUNsYXNzKFxcXCJidG4taW5mb1xcXCIpLnRvZ2dsZUNsYXNzKFxcXCJidG4tZGVmYXVsdFxcXCIpLnRleHQodGV4dCk7XFxuICAgIH07XFxuXFxuICAgIHZhciBmYWlsID0gZnVuY3Rpb24gKCkge1xcbiAgICAgICAgYWxlcnQoXFxcIlNvbWV0aGluZyBmYWlsZWRcXFwiKTtcXG4gICAgfTtcXG5cXG4gICAgcmV0dXJuIHtcXG4gICAgICAgIGluaXQ6IGluaXRcXG4gICAgfVxcblxcbn0oRm9sbG93aW5nU2VydmljZSk7XFxuXCJcblxuXG4vLy8vLy8vLy8vLy8vLy8vLy9cbi8vIFdFQlBBQ0sgRk9PVEVSXG4vLyAuLi9ub2RlX21vZHVsZXMvcmF3LWxvYWRlciEuL2pzL2NvbnRyb2xsZXJzL2V4aGliaXREZXRhaWxzQ29udHJvbGxlci5leGVjLmpzXG4vLyBtb2R1bGUgaWQgPSAxNDFcbi8vIG1vZHVsZSBjaHVua3MgPSA0Il0sInNvdXJjZVJvb3QiOiIifQ==\n//# sourceURL=webpack-internal:///141\n");

/***/ })

/******/ });