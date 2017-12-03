webpackJsonp([10],{

/***/ 3:
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("/* WEBPACK VAR INJECTION */(function($) {\n\nObject.defineProperty(exports, \"__esModule\", {\n  value: true\n});\nexports.default = void 0;\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nfunction _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }\n\nfunction _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }\n\nvar FollowingService =\n/*#__PURE__*/\nfunction () {\n  function FollowingService() {\n    _classCallCheck(this, FollowingService);\n\n    this.createFollowing = this.createFollowing.bind(this);\n    this.deleteFollowing = this.deleteFollowing.bind(this);\n  }\n\n  _createClass(FollowingService, [{\n    key: \"createFollowing\",\n    value: function createFollowing(followeeId, done, fail) {\n      var contentTypeAttribute = 'application/json';\n      var urlAttribute = '/api/followings';\n      var dataAttribute = JSON.stringify({\n        followeeId: followeeId\n      });\n      $.ajax({\n        url: urlAttribute,\n        method: 'POST',\n        contentType: contentTypeAttribute,\n        data: dataAttribute\n      }).done(done).fail(fail);\n    }\n  }, {\n    key: \"deleteFollowing\",\n    value: function deleteFollowing(followeeId, done, fail) {\n      var contentTypeAttribute = 'application/json';\n      var urlAttribute = '/api/followings';\n      var dataAttribute = JSON.stringify({\n        followeeId: followeeId\n      });\n      $.ajax({\n        url: urlAttribute,\n        method: 'DELETE',\n        contentType: contentTypeAttribute,\n        data: dataAttribute\n      }).done(done).fail(fail);\n    }\n  }]);\n\n  return FollowingService;\n}();\n\nexports.default = FollowingService;\n/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9qcy9FeGhpYml0RGV0YWlscy9mb2xsb3dpbmdTZXJ2aWNlLmpzPzRjOGIiXSwibmFtZXMiOlsiRm9sbG93aW5nU2VydmljZSIsImNyZWF0ZUZvbGxvd2luZyIsImJpbmQiLCJkZWxldGVGb2xsb3dpbmciLCJmb2xsb3dlZUlkIiwiZG9uZSIsImZhaWwiLCJjb250ZW50VHlwZUF0dHJpYnV0ZSIsInVybEF0dHJpYnV0ZSIsImRhdGFBdHRyaWJ1dGUiLCJKU09OIiwic3RyaW5naWZ5IiwiJCIsImFqYXgiLCJ1cmwiLCJtZXRob2QiLCJjb250ZW50VHlwZSIsImRhdGEiXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7Ozs7SUFBcUJBLGdCOzs7QUFDbkIsOEJBQWM7QUFBQTs7QUFDWixTQUFLQyxlQUFMLEdBQXVCLEtBQUtBLGVBQUwsQ0FBcUJDLElBQXJCLENBQTBCLElBQTFCLENBQXZCO0FBQ0EsU0FBS0MsZUFBTCxHQUF1QixLQUFLQSxlQUFMLENBQXFCRCxJQUFyQixDQUEwQixJQUExQixDQUF2QjtBQUNEOzs7O29DQUVlRSxVLEVBQVlDLEksRUFBTUMsSSxFQUFNO0FBQ3RDLFVBQU1DLHVCQUF1QixrQkFBN0I7QUFDQSxVQUFNQyxlQUFlLGlCQUFyQjtBQUNBLFVBQU1DLGdCQUFnQkMsS0FBS0MsU0FBTCxDQUFlO0FBQ25DUCxvQkFBWUE7QUFEdUIsT0FBZixDQUF0QjtBQUlBUSxRQUFFQyxJQUFGLENBQU87QUFDTEMsYUFBS04sWUFEQTtBQUVMTyxnQkFBUSxNQUZIO0FBR0xDLHFCQUFhVCxvQkFIUjtBQUlMVSxjQUFNUjtBQUpELE9BQVAsRUFNR0osSUFOSCxDQU1RQSxJQU5SLEVBT0dDLElBUEgsQ0FPUUEsSUFQUjtBQVFEOzs7b0NBRWVGLFUsRUFBWUMsSSxFQUFNQyxJLEVBQU07QUFDdEMsVUFBTUMsdUJBQXVCLGtCQUE3QjtBQUNBLFVBQU1DLGVBQWUsaUJBQXJCO0FBQ0EsVUFBTUMsZ0JBQWdCQyxLQUFLQyxTQUFMLENBQWU7QUFDbkNQLG9CQUFZQTtBQUR1QixPQUFmLENBQXRCO0FBR0FRLFFBQUVDLElBQUYsQ0FBTztBQUNMQyxhQUFLTixZQURBO0FBRUxPLGdCQUFRLFFBRkg7QUFHTEMscUJBQWFULG9CQUhSO0FBSUxVLGNBQU1SO0FBSkQsT0FBUCxFQU1HSixJQU5ILENBTVFBLElBTlIsRUFPR0MsSUFQSCxDQU9RQSxJQVBSO0FBUUQiLCJmaWxlIjoiMy5qcyIsInNvdXJjZXNDb250ZW50IjpbImV4cG9ydCBkZWZhdWx0IGNsYXNzIEZvbGxvd2luZ1NlcnZpY2Uge1xuICBjb25zdHJ1Y3RvcigpIHtcbiAgICB0aGlzLmNyZWF0ZUZvbGxvd2luZyA9IHRoaXMuY3JlYXRlRm9sbG93aW5nLmJpbmQodGhpcyk7XG4gICAgdGhpcy5kZWxldGVGb2xsb3dpbmcgPSB0aGlzLmRlbGV0ZUZvbGxvd2luZy5iaW5kKHRoaXMpO1xuICB9XG5cbiAgY3JlYXRlRm9sbG93aW5nKGZvbGxvd2VlSWQsIGRvbmUsIGZhaWwpIHtcbiAgICBjb25zdCBjb250ZW50VHlwZUF0dHJpYnV0ZSA9ICdhcHBsaWNhdGlvbi9qc29uJztcbiAgICBjb25zdCB1cmxBdHRyaWJ1dGUgPSAnL2FwaS9mb2xsb3dpbmdzJztcbiAgICBjb25zdCBkYXRhQXR0cmlidXRlID0gSlNPTi5zdHJpbmdpZnkoe1xuICAgICAgZm9sbG93ZWVJZDogZm9sbG93ZWVJZFxuICAgIH0pO1xuXG4gICAgJC5hamF4KHtcbiAgICAgIHVybDogdXJsQXR0cmlidXRlLFxuICAgICAgbWV0aG9kOiAnUE9TVCcsXG4gICAgICBjb250ZW50VHlwZTogY29udGVudFR5cGVBdHRyaWJ1dGUsXG4gICAgICBkYXRhOiBkYXRhQXR0cmlidXRlXG4gICAgfSlcbiAgICAgIC5kb25lKGRvbmUpXG4gICAgICAuZmFpbChmYWlsKTtcbiAgfVxuXG4gIGRlbGV0ZUZvbGxvd2luZyhmb2xsb3dlZUlkLCBkb25lLCBmYWlsKSB7XG4gICAgY29uc3QgY29udGVudFR5cGVBdHRyaWJ1dGUgPSAnYXBwbGljYXRpb24vanNvbic7XG4gICAgY29uc3QgdXJsQXR0cmlidXRlID0gJy9hcGkvZm9sbG93aW5ncyc7XG4gICAgY29uc3QgZGF0YUF0dHJpYnV0ZSA9IEpTT04uc3RyaW5naWZ5KHtcbiAgICAgIGZvbGxvd2VlSWQ6IGZvbGxvd2VlSWRcbiAgICB9KTtcbiAgICAkLmFqYXgoe1xuICAgICAgdXJsOiB1cmxBdHRyaWJ1dGUsXG4gICAgICBtZXRob2Q6ICdERUxFVEUnLFxuICAgICAgY29udGVudFR5cGU6IGNvbnRlbnRUeXBlQXR0cmlidXRlLFxuICAgICAgZGF0YTogZGF0YUF0dHJpYnV0ZVxuICAgIH0pXG4gICAgICAuZG9uZShkb25lKVxuICAgICAgLmZhaWwoZmFpbCk7XG4gIH1cbn1cblxuXG5cbi8vIFdFQlBBQ0sgRk9PVEVSIC8vXG4vLyAuL2pzL0V4aGliaXREZXRhaWxzL2ZvbGxvd2luZ1NlcnZpY2UuanMiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///3\n");

/***/ })

},[3]);