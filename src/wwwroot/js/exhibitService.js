webpackJsonp([9],{

/***/ 2:
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("/* WEBPACK VAR INJECTION */(function($) {\n\nObject.defineProperty(exports, \"__esModule\", {\n  value: true\n});\nexports.default = void 0;\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nfunction _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }\n\nfunction _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }\n\nvar exhibitService =\n/*#__PURE__*/\nfunction () {\n  function exhibitService() {\n    _classCallCheck(this, exhibitService);\n\n    this.cancelExhibit = this.cancelExhibit.bind(this);\n  }\n\n  _createClass(exhibitService, [{\n    key: \"cancelExhibit\",\n    value: function cancelExhibit(exhibitId, done, fail) {\n      var contentTypeAttribute = 'application/json';\n      var urlAttribute = '/api/exhibits';\n      var dataAttribute = JSON.stringify({\n        Id: exhibitId\n      });\n      $.ajax({\n        url: urlAttribute,\n        method: 'DELETE',\n        contentType: contentTypeAttribute,\n        data: dataAttribute\n      }).done(done).fail(fail);\n    }\n  }]);\n\n  return exhibitService;\n}();\n\nexports.default = exhibitService;\n/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(1)))//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9FeGhpYml0Q2FuY2VsL2V4aGliaXRTZXJ2aWNlLmpzP2YzNDkiXSwibmFtZXMiOlsiZXhoaWJpdFNlcnZpY2UiLCJjYW5jZWxFeGhpYml0IiwiYmluZCIsImV4aGliaXRJZCIsImRvbmUiLCJmYWlsIiwiY29udGVudFR5cGVBdHRyaWJ1dGUiLCJ1cmxBdHRyaWJ1dGUiLCJkYXRhQXR0cmlidXRlIiwiSlNPTiIsInN0cmluZ2lmeSIsIklkIiwiJCIsImFqYXgiLCJ1cmwiLCJtZXRob2QiLCJjb250ZW50VHlwZSIsImRhdGEiXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7Ozs7SUFBcUJBLGM7OztBQUNuQiw0QkFBYztBQUFBOztBQUNaLFNBQUtDLGFBQUwsR0FBcUIsS0FBS0EsYUFBTCxDQUFtQkMsSUFBbkIsQ0FBd0IsSUFBeEIsQ0FBckI7QUFDRDs7OztrQ0FFYUMsUyxFQUFXQyxJLEVBQU1DLEksRUFBTTtBQUNuQyxVQUFNQyx1QkFBdUIsa0JBQTdCO0FBQ0EsVUFBTUMsZUFBZSxlQUFyQjtBQUNBLFVBQU1DLGdCQUFnQkMsS0FBS0MsU0FBTCxDQUFlO0FBQ25DQyxZQUFJUjtBQUQrQixPQUFmLENBQXRCO0FBSUFTLFFBQUVDLElBQUYsQ0FBTztBQUNMQyxhQUFLUCxZQURBO0FBRUxRLGdCQUFRLFFBRkg7QUFHTEMscUJBQWFWLG9CQUhSO0FBSUxXLGNBQU1UO0FBSkQsT0FBUCxFQU1HSixJQU5ILENBTVFBLElBTlIsRUFPR0MsSUFQSCxDQU9RQSxJQVBSO0FBUUQiLCJmaWxlIjoiMi5qcyIsInNvdXJjZXNDb250ZW50IjpbImV4cG9ydCBkZWZhdWx0IGNsYXNzIGV4aGliaXRTZXJ2aWNlIHtcbiAgY29uc3RydWN0b3IoKSB7XG4gICAgdGhpcy5jYW5jZWxFeGhpYml0ID0gdGhpcy5jYW5jZWxFeGhpYml0LmJpbmQodGhpcyk7XG4gIH1cblxuICBjYW5jZWxFeGhpYml0KGV4aGliaXRJZCwgZG9uZSwgZmFpbCkge1xuICAgIGNvbnN0IGNvbnRlbnRUeXBlQXR0cmlidXRlID0gJ2FwcGxpY2F0aW9uL2pzb24nO1xuICAgIGNvbnN0IHVybEF0dHJpYnV0ZSA9ICcvYXBpL2V4aGliaXRzJztcbiAgICBjb25zdCBkYXRhQXR0cmlidXRlID0gSlNPTi5zdHJpbmdpZnkoe1xuICAgICAgSWQ6IGV4aGliaXRJZFxuICAgIH0pO1xuXG4gICAgJC5hamF4KHtcbiAgICAgIHVybDogdXJsQXR0cmlidXRlLFxuICAgICAgbWV0aG9kOiAnREVMRVRFJyxcbiAgICAgIGNvbnRlbnRUeXBlOiBjb250ZW50VHlwZUF0dHJpYnV0ZSxcbiAgICAgIGRhdGE6IGRhdGFBdHRyaWJ1dGVcbiAgICB9KVxuICAgICAgLmRvbmUoZG9uZSlcbiAgICAgIC5mYWlsKGZhaWwpO1xuICB9XG59XG5cblxuXG4vLyBXRUJQQUNLIEZPT1RFUiAvL1xuLy8gLi9FeGhpYml0Q2FuY2VsL2V4aGliaXRTZXJ2aWNlLmpzIl0sInNvdXJjZVJvb3QiOiIifQ==\n//# sourceURL=webpack-internal:///2\n");

/***/ })

},[2]);