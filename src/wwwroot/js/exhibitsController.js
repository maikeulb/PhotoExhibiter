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
/******/ 	return __webpack_require__(__webpack_require__.s = 142);
/******/ })
/************************************************************************/
/******/ ({

/***/ 1:
/***/ (function(module, exports) {

eval("/*\n\tMIT License http://www.opensource.org/licenses/mit-license.php\n\tAuthor Tobias Koppers @sokra\n*/\nmodule.exports = function(src) {\n\tfunction log(error) {\n\t\t(typeof console !== \"undefined\")\n\t\t&& (console.error || console.log)(\"[Script Loader]\", error);\n\t}\n\n\t// Check for IE =< 8\n\tfunction isIE() {\n\t\treturn typeof attachEvent !== \"undefined\" && typeof addEventListener === \"undefined\";\n\t}\n\n\ttry {\n\t\tif (typeof execScript !== \"undefined\" && isIE()) {\n\t\t\texecScript(src);\n\t\t} else if (typeof eval !== \"undefined\") {\n\t\t\teval.call(null, src);\n\t\t} else {\n\t\t\tlog(\"EvalError: No eval function available\");\n\t\t}\n\t} catch (error) {\n\t\tlog(error);\n\t}\n}\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi4vbm9kZV9tb2R1bGVzL3NjcmlwdC1sb2FkZXIvYWRkU2NyaXB0LmpzP2E2ODkiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQUVBO0FBQ0E7QUFDQTtBQUNBOztBQUVBO0FBQ0E7QUFDQTtBQUNBLEdBQUc7QUFDSDtBQUNBLEdBQUc7QUFDSDtBQUNBO0FBQ0EsRUFBRTtBQUNGO0FBQ0E7QUFDQSIsImZpbGUiOiIxLmpzIiwic291cmNlc0NvbnRlbnQiOlsiLypcblx0TUlUIExpY2Vuc2UgaHR0cDovL3d3dy5vcGVuc291cmNlLm9yZy9saWNlbnNlcy9taXQtbGljZW5zZS5waHBcblx0QXV0aG9yIFRvYmlhcyBLb3BwZXJzIEBzb2tyYVxuKi9cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24oc3JjKSB7XG5cdGZ1bmN0aW9uIGxvZyhlcnJvcikge1xuXHRcdCh0eXBlb2YgY29uc29sZSAhPT0gXCJ1bmRlZmluZWRcIilcblx0XHQmJiAoY29uc29sZS5lcnJvciB8fCBjb25zb2xlLmxvZykoXCJbU2NyaXB0IExvYWRlcl1cIiwgZXJyb3IpO1xuXHR9XG5cblx0Ly8gQ2hlY2sgZm9yIElFID08IDhcblx0ZnVuY3Rpb24gaXNJRSgpIHtcblx0XHRyZXR1cm4gdHlwZW9mIGF0dGFjaEV2ZW50ICE9PSBcInVuZGVmaW5lZFwiICYmIHR5cGVvZiBhZGRFdmVudExpc3RlbmVyID09PSBcInVuZGVmaW5lZFwiO1xuXHR9XG5cblx0dHJ5IHtcblx0XHRpZiAodHlwZW9mIGV4ZWNTY3JpcHQgIT09IFwidW5kZWZpbmVkXCIgJiYgaXNJRSgpKSB7XG5cdFx0XHRleGVjU2NyaXB0KHNyYyk7XG5cdFx0fSBlbHNlIGlmICh0eXBlb2YgZXZhbCAhPT0gXCJ1bmRlZmluZWRcIikge1xuXHRcdFx0ZXZhbC5jYWxsKG51bGwsIHNyYyk7XG5cdFx0fSBlbHNlIHtcblx0XHRcdGxvZyhcIkV2YWxFcnJvcjogTm8gZXZhbCBmdW5jdGlvbiBhdmFpbGFibGVcIik7XG5cdFx0fVxuXHR9IGNhdGNoIChlcnJvcikge1xuXHRcdGxvZyhlcnJvcik7XG5cdH1cbn1cblxuXG5cbi8vLy8vLy8vLy8vLy8vLy8vL1xuLy8gV0VCUEFDSyBGT09URVJcbi8vIC4uL25vZGVfbW9kdWxlcy9zY3JpcHQtbG9hZGVyL2FkZFNjcmlwdC5qc1xuLy8gbW9kdWxlIGlkID0gMVxuLy8gbW9kdWxlIGNodW5rcyA9IDEgMiAzIDQiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///1\n");

/***/ }),

/***/ 142:
/***/ (function(module, exports, __webpack_require__) {

eval("__webpack_require__(1)(__webpack_require__(143))//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9qcy9jb250cm9sbGVycy9leGhpYml0c0NvbnRyb2xsZXIuZXhlYy5qcz83ZDQxIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBIiwiZmlsZSI6IjE0Mi5qcyIsInNvdXJjZXNDb250ZW50IjpbInJlcXVpcmUoXCIhIS9ob21lL21pY2hhZWwvRG90TmV0UHJvamVjdHMvUGhvdG9FeGhpYml0ZXIvc3JjL25vZGVfbW9kdWxlcy9zY3JpcHQtbG9hZGVyL2FkZFNjcmlwdC5qc1wiKShyZXF1aXJlKFwiISEvaG9tZS9taWNoYWVsL0RvdE5ldFByb2plY3RzL1Bob3RvRXhoaWJpdGVyL3NyYy9ub2RlX21vZHVsZXMvcmF3LWxvYWRlci9pbmRleC5qcyEvaG9tZS9taWNoYWVsL0RvdE5ldFByb2plY3RzL1Bob3RvRXhoaWJpdGVyL3NyYy9DbGllbnQvanMvY29udHJvbGxlcnMvZXhoaWJpdHNDb250cm9sbGVyLmV4ZWMuanNcIikpXG5cblxuLy8vLy8vLy8vLy8vLy8vLy8vXG4vLyBXRUJQQUNLIEZPT1RFUlxuLy8gLi9qcy9jb250cm9sbGVycy9leGhpYml0c0NvbnRyb2xsZXIuZXhlYy5qc1xuLy8gbW9kdWxlIGlkID0gMTQyXG4vLyBtb2R1bGUgY2h1bmtzID0gMyJdLCJzb3VyY2VSb290IjoiIn0=\n//# sourceURL=webpack-internal:///142\n");

/***/ }),

/***/ 143:
/***/ (function(module, exports) {

eval("module.exports = \"var ExhibitsController = function (attendanceService) {\\n    var button;\\n\\n    var init = function (container) {\\n        $(container).on(\\\"click\\\", \\\".js-toggle-attendance\\\", toggleAttendance);\\n    };\\n\\n    var toggleAttendance = function (e) {\\n        button = $(e.target);\\n\\n        var exhibitId = button.attr(\\\"data-exhibit-id\\\");\\n\\n        if (button.hasClass(\\\"btn-default\\\"))\\n            attendanceService.createAttendance(exhibitId, done, fail);\\n        else\\n            attendanceService.deleteAttendance(exhibitId, done, fail);\\n    };\\n\\n    var done = function () {\\n        var text = (button.text() == \\\"Going\\\") ? \\\"Going?\\\" : \\\"Going\\\";\\n\\n        button.toggleClass(\\\"btn-info\\\").toggleClass(\\\"btn-default\\\").text(text);\\n    };\\n\\n    var fail = function () {\\n        alert(\\\"Something failed\\\");\\n    };\\n\\n    return {\\n        init: init\\n    }\\n\\n}(AttendanceService);\\n\"//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9qcy9jb250cm9sbGVycy9leGhpYml0c0NvbnRyb2xsZXIuZXhlYy5qcz81NGQ1Il0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBLHlFQUF5RSxpQkFBaUIseUNBQXlDLGtGQUFrRixRQUFRLDZDQUE2QywrQkFBK0IsNkRBQTZELHlIQUF5SCxzRkFBc0YsUUFBUSxnQ0FBZ0MsMkVBQTJFLHFGQUFxRixRQUFRLGdDQUFnQyxzQ0FBc0MsUUFBUSxnQkFBZ0IsMkJBQTJCLEtBQUssb0JBQW9CIiwiZmlsZSI6IjE0My5qcyIsInNvdXJjZXNDb250ZW50IjpbIm1vZHVsZS5leHBvcnRzID0gXCJ2YXIgRXhoaWJpdHNDb250cm9sbGVyID0gZnVuY3Rpb24gKGF0dGVuZGFuY2VTZXJ2aWNlKSB7XFxuICAgIHZhciBidXR0b247XFxuXFxuICAgIHZhciBpbml0ID0gZnVuY3Rpb24gKGNvbnRhaW5lcikge1xcbiAgICAgICAgJChjb250YWluZXIpLm9uKFxcXCJjbGlja1xcXCIsIFxcXCIuanMtdG9nZ2xlLWF0dGVuZGFuY2VcXFwiLCB0b2dnbGVBdHRlbmRhbmNlKTtcXG4gICAgfTtcXG5cXG4gICAgdmFyIHRvZ2dsZUF0dGVuZGFuY2UgPSBmdW5jdGlvbiAoZSkge1xcbiAgICAgICAgYnV0dG9uID0gJChlLnRhcmdldCk7XFxuXFxuICAgICAgICB2YXIgZXhoaWJpdElkID0gYnV0dG9uLmF0dHIoXFxcImRhdGEtZXhoaWJpdC1pZFxcXCIpO1xcblxcbiAgICAgICAgaWYgKGJ1dHRvbi5oYXNDbGFzcyhcXFwiYnRuLWRlZmF1bHRcXFwiKSlcXG4gICAgICAgICAgICBhdHRlbmRhbmNlU2VydmljZS5jcmVhdGVBdHRlbmRhbmNlKGV4aGliaXRJZCwgZG9uZSwgZmFpbCk7XFxuICAgICAgICBlbHNlXFxuICAgICAgICAgICAgYXR0ZW5kYW5jZVNlcnZpY2UuZGVsZXRlQXR0ZW5kYW5jZShleGhpYml0SWQsIGRvbmUsIGZhaWwpO1xcbiAgICB9O1xcblxcbiAgICB2YXIgZG9uZSA9IGZ1bmN0aW9uICgpIHtcXG4gICAgICAgIHZhciB0ZXh0ID0gKGJ1dHRvbi50ZXh0KCkgPT0gXFxcIkdvaW5nXFxcIikgPyBcXFwiR29pbmc/XFxcIiA6IFxcXCJHb2luZ1xcXCI7XFxuXFxuICAgICAgICBidXR0b24udG9nZ2xlQ2xhc3MoXFxcImJ0bi1pbmZvXFxcIikudG9nZ2xlQ2xhc3MoXFxcImJ0bi1kZWZhdWx0XFxcIikudGV4dCh0ZXh0KTtcXG4gICAgfTtcXG5cXG4gICAgdmFyIGZhaWwgPSBmdW5jdGlvbiAoKSB7XFxuICAgICAgICBhbGVydChcXFwiU29tZXRoaW5nIGZhaWxlZFxcXCIpO1xcbiAgICB9O1xcblxcbiAgICByZXR1cm4ge1xcbiAgICAgICAgaW5pdDogaW5pdFxcbiAgICB9XFxuXFxufShBdHRlbmRhbmNlU2VydmljZSk7XFxuXCJcblxuXG4vLy8vLy8vLy8vLy8vLy8vLy9cbi8vIFdFQlBBQ0sgRk9PVEVSXG4vLyAuLi9ub2RlX21vZHVsZXMvcmF3LWxvYWRlciEuL2pzL2NvbnRyb2xsZXJzL2V4aGliaXRzQ29udHJvbGxlci5leGVjLmpzXG4vLyBtb2R1bGUgaWQgPSAxNDNcbi8vIG1vZHVsZSBjaHVua3MgPSAzIl0sInNvdXJjZVJvb3QiOiIifQ==\n//# sourceURL=webpack-internal:///143\n");

/***/ })

/******/ });