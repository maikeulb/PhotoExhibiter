webpackJsonp([2,4,6],{

/***/ 139:
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\n\nvar _exhibitDetailsController = _interopRequireDefault(__webpack_require__(4));\n\nvar _followingService = _interopRequireDefault(__webpack_require__(2));\n\nvar _jquery = __webpack_require__(1);\n\nfunction _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }\n\nvar followingService = new _followingService.default();\nvar exhibitDetailsController = new _exhibitDetailsController.default(followingService);\nexhibitDetailsController.init(); // $(document).ready(() => controller.init());//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9FeGhpYml0RGV0YWlscy9leGhpYml0RGV0YWlscy5qcz9kYzUwIl0sIm5hbWVzIjpbImZvbGxvd2luZ1NlcnZpY2UiLCJleGhpYml0RGV0YWlsc0NvbnRyb2xsZXIiLCJpbml0Il0sIm1hcHBpbmdzIjoiOztBQUFBOztBQUNBOztBQUNBOzs7O0FBRUEsSUFBTUEsbUJBQW1CLCtCQUF6QjtBQUNBLElBQU1DLDJCQUEyQixzQ0FBNkJELGdCQUE3QixDQUFqQztBQUNBQyx5QkFBeUJDLElBQXpCLEcsQ0FDQSIsImZpbGUiOiIxMzkuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgRXhoaWJpdERldGFpbHNDb250cm9sbGVyIGZyb20gJy4vZXhoaWJpdERldGFpbHNDb250cm9sbGVyJztcbmltcG9ydCBGb2xsb3dpbmdTZXJ2aWNlIGZyb20gJy4vZm9sbG93aW5nU2VydmljZSc7XG5pbXBvcnQgeyAkLCBqUXVlcnkgfSBmcm9tICdqcXVlcnknO1xuXG5jb25zdCBmb2xsb3dpbmdTZXJ2aWNlID0gbmV3IEZvbGxvd2luZ1NlcnZpY2UoKTtcbmNvbnN0IGV4aGliaXREZXRhaWxzQ29udHJvbGxlciA9IG5ldyBFeGhpYml0RGV0YWlsc0NvbnRyb2xsZXIoZm9sbG93aW5nU2VydmljZSk7XG5leGhpYml0RGV0YWlsc0NvbnRyb2xsZXIuaW5pdCgpO1xuLy8gJChkb2N1bWVudCkucmVhZHkoKCkgPT4gY29udHJvbGxlci5pbml0KCkpO1xuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vRXhoaWJpdERldGFpbHMvZXhoaWJpdERldGFpbHMuanMiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///139\n");

/***/ }),

/***/ 2:
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("/* WEBPACK VAR INJECTION */(function($) {\n\nObject.defineProperty(exports, \"__esModule\", {\n  value: true\n});\nexports.default = void 0;\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nfunction _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }\n\nfunction _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }\n\nvar FollowingService =\n/*#__PURE__*/\nfunction () {\n  function FollowingService() {\n    _classCallCheck(this, FollowingService);\n\n    this.createFollowing = this.createFollowing.bind(this);\n    this.deleteFollowing = this.deleteFollowing.bind(this);\n  }\n\n  _createClass(FollowingService, [{\n    key: \"createFollowing\",\n    value: function createFollowing(followeeId, done, fail) {\n      var contentTypeAttribute = 'application/json';\n      var urlAttribute = '/api/followings';\n      var dataAttribute = JSON.stringify({\n        followeeId: followeeId\n      });\n      return $.ajax({\n        url: urlAttribute,\n        method: 'POST',\n        contentType: contentTypeAttribute,\n        data: dataAttribute\n      }).done(done).fail(fail);\n    }\n  }, {\n    key: \"deleteFollowing\",\n    value: function deleteFollowing(followeeId, done, fail) {\n      var contentTypeAttribute = 'application/json';\n      var urlAttribute = '/api/followings';\n      var dataAttribute = JSON.stringify({\n        followeeId: followeeId\n      });\n      $.ajax({\n        url: urlAttribute,\n        method: 'DELETE',\n        contentType: contentTypeAttribute,\n        data: dataAttribute\n      }).done(done).fail(fail);\n    }\n  }]);\n\n  return FollowingService;\n}();\n\nexports.default = FollowingService;\n/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(1)))//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9FeGhpYml0RGV0YWlscy9mb2xsb3dpbmdTZXJ2aWNlLmpzPzY3YjAiXSwibmFtZXMiOlsiRm9sbG93aW5nU2VydmljZSIsImNyZWF0ZUZvbGxvd2luZyIsImJpbmQiLCJkZWxldGVGb2xsb3dpbmciLCJmb2xsb3dlZUlkIiwiZG9uZSIsImZhaWwiLCJjb250ZW50VHlwZUF0dHJpYnV0ZSIsInVybEF0dHJpYnV0ZSIsImRhdGFBdHRyaWJ1dGUiLCJKU09OIiwic3RyaW5naWZ5IiwiJCIsImFqYXgiLCJ1cmwiLCJtZXRob2QiLCJjb250ZW50VHlwZSIsImRhdGEiXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7Ozs7SUFBcUJBLGdCOzs7QUFDbkIsOEJBQWM7QUFBQTs7QUFDWixTQUFLQyxlQUFMLEdBQXVCLEtBQUtBLGVBQUwsQ0FBcUJDLElBQXJCLENBQTBCLElBQTFCLENBQXZCO0FBQ0EsU0FBS0MsZUFBTCxHQUF1QixLQUFLQSxlQUFMLENBQXFCRCxJQUFyQixDQUEwQixJQUExQixDQUF2QjtBQUNEOzs7O29DQUVlRSxVLEVBQVlDLEksRUFBTUMsSSxFQUFNO0FBQ3RDLFVBQU1DLHVCQUF1QixrQkFBN0I7QUFDQSxVQUFNQyxlQUFlLGlCQUFyQjtBQUNBLFVBQU1DLGdCQUFnQkMsS0FBS0MsU0FBTCxDQUFlO0FBQ25DUCxvQkFBWUE7QUFEdUIsT0FBZixDQUF0QjtBQUdBLGFBQU9RLEVBQUVDLElBQUYsQ0FBTztBQUNaQyxhQUFLTixZQURPO0FBRVpPLGdCQUFRLE1BRkk7QUFHWkMscUJBQWFULG9CQUhEO0FBSVpVLGNBQU1SO0FBSk0sT0FBUCxFQU1KSixJQU5JLENBTUNBLElBTkQsRUFPSkMsSUFQSSxDQU9DQSxJQVBELENBQVA7QUFRRDs7O29DQUVlRixVLEVBQVlDLEksRUFBTUMsSSxFQUFNO0FBQ3RDLFVBQU1DLHVCQUF1QixrQkFBN0I7QUFDQSxVQUFNQyxlQUFlLGlCQUFyQjtBQUNBLFVBQU1DLGdCQUFnQkMsS0FBS0MsU0FBTCxDQUFlO0FBQ25DUCxvQkFBWUE7QUFEdUIsT0FBZixDQUF0QjtBQUdBUSxRQUFFQyxJQUFGLENBQU87QUFDTEMsYUFBS04sWUFEQTtBQUVMTyxnQkFBUSxRQUZIO0FBR0xDLHFCQUFhVCxvQkFIUjtBQUlMVSxjQUFNUjtBQUpELE9BQVAsRUFNR0osSUFOSCxDQU1RQSxJQU5SLEVBT0dDLElBUEgsQ0FPUUEsSUFQUjtBQVFEIiwiZmlsZSI6IjIuanMiLCJzb3VyY2VzQ29udGVudCI6WyJleHBvcnQgZGVmYXVsdCBjbGFzcyBGb2xsb3dpbmdTZXJ2aWNlIHtcbiAgY29uc3RydWN0b3IoKSB7XG4gICAgdGhpcy5jcmVhdGVGb2xsb3dpbmcgPSB0aGlzLmNyZWF0ZUZvbGxvd2luZy5iaW5kKHRoaXMpO1xuICAgIHRoaXMuZGVsZXRlRm9sbG93aW5nID0gdGhpcy5kZWxldGVGb2xsb3dpbmcuYmluZCh0aGlzKTtcbiAgfVxuXG4gIGNyZWF0ZUZvbGxvd2luZyhmb2xsb3dlZUlkLCBkb25lLCBmYWlsKSB7XG4gICAgY29uc3QgY29udGVudFR5cGVBdHRyaWJ1dGUgPSAnYXBwbGljYXRpb24vanNvbic7XG4gICAgY29uc3QgdXJsQXR0cmlidXRlID0gJy9hcGkvZm9sbG93aW5ncyc7XG4gICAgY29uc3QgZGF0YUF0dHJpYnV0ZSA9IEpTT04uc3RyaW5naWZ5KHtcbiAgICAgIGZvbGxvd2VlSWQ6IGZvbGxvd2VlSWRcbiAgICB9KTtcbiAgICByZXR1cm4gJC5hamF4KHtcbiAgICAgIHVybDogdXJsQXR0cmlidXRlLFxuICAgICAgbWV0aG9kOiAnUE9TVCcsXG4gICAgICBjb250ZW50VHlwZTogY29udGVudFR5cGVBdHRyaWJ1dGUsXG4gICAgICBkYXRhOiBkYXRhQXR0cmlidXRlXG4gICAgfSlcbiAgICAgIC5kb25lKGRvbmUpXG4gICAgICAuZmFpbChmYWlsKTtcbiAgfVxuXG4gIGRlbGV0ZUZvbGxvd2luZyhmb2xsb3dlZUlkLCBkb25lLCBmYWlsKSB7XG4gICAgY29uc3QgY29udGVudFR5cGVBdHRyaWJ1dGUgPSAnYXBwbGljYXRpb24vanNvbic7XG4gICAgY29uc3QgdXJsQXR0cmlidXRlID0gJy9hcGkvZm9sbG93aW5ncyc7XG4gICAgY29uc3QgZGF0YUF0dHJpYnV0ZSA9IEpTT04uc3RyaW5naWZ5KHtcbiAgICAgIGZvbGxvd2VlSWQ6IGZvbGxvd2VlSWRcbiAgICB9KTtcbiAgICAkLmFqYXgoe1xuICAgICAgdXJsOiB1cmxBdHRyaWJ1dGUsXG4gICAgICBtZXRob2Q6ICdERUxFVEUnLFxuICAgICAgY29udGVudFR5cGU6IGNvbnRlbnRUeXBlQXR0cmlidXRlLFxuICAgICAgZGF0YTogZGF0YUF0dHJpYnV0ZVxuICAgIH0pXG4gICAgICAuZG9uZShkb25lKVxuICAgICAgLmZhaWwoZmFpbCk7XG4gIH1cbn1cblxuXG5cbi8vIFdFQlBBQ0sgRk9PVEVSIC8vXG4vLyAuL0V4aGliaXREZXRhaWxzL2ZvbGxvd2luZ1NlcnZpY2UuanMiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///2\n");

/***/ }),

/***/ 4:
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("/* WEBPACK VAR INJECTION */(function($) {\n\nObject.defineProperty(exports, \"__esModule\", {\n  value: true\n});\nexports.default = void 0;\n\nvar _followingService = _interopRequireDefault(__webpack_require__(2));\n\nfunction _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nfunction _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }\n\nfunction _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }\n\nvar ExhibitDetailsController =\n/*#__PURE__*/\nfunction () {\n  function ExhibitDetailsController(followingService) {\n    _classCallCheck(this, ExhibitDetailsController);\n\n    this.followingService = followingService;\n    this.followButton = null;\n  }\n\n  _createClass(ExhibitDetailsController, [{\n    key: \"init\",\n    value: function init() {\n      $('.js-toggle-follow').click(this.toggleFollowing.bind(this));\n    }\n  }, {\n    key: \"toggleFollowing\",\n    value: function toggleFollowing(e) {\n      this.followButton = $(e.target);\n      var followeeId = this.followButton.attr('data-user-id');\n      if (this.followButton.hasClass('btn-secondary')) this.followingService.createFollowing(followeeId, this.done.bind(this), this.fail.bind(this));else this.followingService.deleteFollowing(followeeId, this.done.bind(this), this.fail.bind(this));\n    }\n  }, {\n    key: \"done\",\n    value: function done() {\n      var text = this.followButton.text() == 'Follow' ? 'Following' : 'Follow';\n      return this.followButton.toggleClass('btn-info').toggleClass('btn-secondary').text(text);\n    }\n  }, {\n    key: \"fail\",\n    value: function fail() {\n      return alert('Something failed');\n    }\n  }]);\n\n  return ExhibitDetailsController;\n}();\n\nexports.default = ExhibitDetailsController;\n/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(1)))//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9FeGhpYml0RGV0YWlscy9leGhpYml0RGV0YWlsc0NvbnRyb2xsZXIuanM/ZWMxMiJdLCJuYW1lcyI6WyJFeGhpYml0RGV0YWlsc0NvbnRyb2xsZXIiLCJmb2xsb3dpbmdTZXJ2aWNlIiwiZm9sbG93QnV0dG9uIiwiJCIsImNsaWNrIiwidG9nZ2xlRm9sbG93aW5nIiwiYmluZCIsImUiLCJ0YXJnZXQiLCJmb2xsb3dlZUlkIiwiYXR0ciIsImhhc0NsYXNzIiwiY3JlYXRlRm9sbG93aW5nIiwiZG9uZSIsImZhaWwiLCJkZWxldGVGb2xsb3dpbmciLCJ0ZXh0IiwidG9nZ2xlQ2xhc3MiLCJhbGVydCJdLCJtYXBwaW5ncyI6Ijs7Ozs7OztBQUFBOzs7Ozs7Ozs7O0lBRXFCQSx3Qjs7O0FBQ25CLG9DQUFZQyxnQkFBWixFQUE4QjtBQUFBOztBQUM1QixTQUFLQSxnQkFBTCxHQUF3QkEsZ0JBQXhCO0FBQ0EsU0FBS0MsWUFBTCxHQUFvQixJQUFwQjtBQUNEOzs7OzJCQUVNO0FBQ0xDLFFBQUUsbUJBQUYsRUFBdUJDLEtBQXZCLENBQTZCLEtBQUtDLGVBQUwsQ0FBcUJDLElBQXJCLENBQTBCLElBQTFCLENBQTdCO0FBQ0Q7OztvQ0FFZUMsQyxFQUFHO0FBQ2pCLFdBQUtMLFlBQUwsR0FBb0JDLEVBQUVJLEVBQUVDLE1BQUosQ0FBcEI7QUFDQSxVQUFNQyxhQUFhLEtBQUtQLFlBQUwsQ0FBa0JRLElBQWxCLENBQXVCLGNBQXZCLENBQW5CO0FBQ0EsVUFBSSxLQUFLUixZQUFMLENBQWtCUyxRQUFsQixDQUEyQixlQUEzQixDQUFKLEVBQ0UsS0FBS1YsZ0JBQUwsQ0FBc0JXLGVBQXRCLENBQ0VILFVBREYsRUFFRSxLQUFLSSxJQUFMLENBQVVQLElBQVYsQ0FBZSxJQUFmLENBRkYsRUFHRSxLQUFLUSxJQUFMLENBQVVSLElBQVYsQ0FBZSxJQUFmLENBSEYsRUFERixLQU9FLEtBQUtMLGdCQUFMLENBQXNCYyxlQUF0QixDQUNFTixVQURGLEVBRUUsS0FBS0ksSUFBTCxDQUFVUCxJQUFWLENBQWUsSUFBZixDQUZGLEVBR0UsS0FBS1EsSUFBTCxDQUFVUixJQUFWLENBQWUsSUFBZixDQUhGO0FBS0g7OzsyQkFFTTtBQUNMLFVBQU1VLE9BQU8sS0FBS2QsWUFBTCxDQUFrQmMsSUFBbEIsTUFBNEIsUUFBNUIsR0FBdUMsV0FBdkMsR0FBcUQsUUFBbEU7QUFDQSxhQUFPLEtBQUtkLFlBQUwsQ0FDSmUsV0FESSxDQUNRLFVBRFIsRUFFSkEsV0FGSSxDQUVRLGVBRlIsRUFHSkQsSUFISSxDQUdDQSxJQUhELENBQVA7QUFJRDs7OzJCQUVNO0FBQ0wsYUFBT0UsTUFBTSxrQkFBTixDQUFQO0FBQ0QiLCJmaWxlIjoiNC5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCBmb2xsb3dpbmdTZXJ2aWNlIGZyb20gJy4vZm9sbG93aW5nU2VydmljZSc7XG5cbmV4cG9ydCBkZWZhdWx0IGNsYXNzIEV4aGliaXREZXRhaWxzQ29udHJvbGxlciB7XG4gIGNvbnN0cnVjdG9yKGZvbGxvd2luZ1NlcnZpY2UpIHtcbiAgICB0aGlzLmZvbGxvd2luZ1NlcnZpY2UgPSBmb2xsb3dpbmdTZXJ2aWNlO1xuICAgIHRoaXMuZm9sbG93QnV0dG9uID0gbnVsbDtcbiAgfVxuXG4gIGluaXQoKSB7XG4gICAgJCgnLmpzLXRvZ2dsZS1mb2xsb3cnKS5jbGljayh0aGlzLnRvZ2dsZUZvbGxvd2luZy5iaW5kKHRoaXMpKTtcbiAgfVxuXG4gIHRvZ2dsZUZvbGxvd2luZyhlKSB7XG4gICAgdGhpcy5mb2xsb3dCdXR0b24gPSAkKGUudGFyZ2V0KTtcbiAgICBjb25zdCBmb2xsb3dlZUlkID0gdGhpcy5mb2xsb3dCdXR0b24uYXR0cignZGF0YS11c2VyLWlkJyk7XG4gICAgaWYgKHRoaXMuZm9sbG93QnV0dG9uLmhhc0NsYXNzKCdidG4tc2Vjb25kYXJ5JykpXG4gICAgICB0aGlzLmZvbGxvd2luZ1NlcnZpY2UuY3JlYXRlRm9sbG93aW5nKFxuICAgICAgICBmb2xsb3dlZUlkLFxuICAgICAgICB0aGlzLmRvbmUuYmluZCh0aGlzKSxcbiAgICAgICAgdGhpcy5mYWlsLmJpbmQodGhpcylcbiAgICAgICk7XG4gICAgZWxzZVxuICAgICAgdGhpcy5mb2xsb3dpbmdTZXJ2aWNlLmRlbGV0ZUZvbGxvd2luZyhcbiAgICAgICAgZm9sbG93ZWVJZCxcbiAgICAgICAgdGhpcy5kb25lLmJpbmQodGhpcyksXG4gICAgICAgIHRoaXMuZmFpbC5iaW5kKHRoaXMpXG4gICAgICApO1xuICB9XG5cbiAgZG9uZSgpIHtcbiAgICBjb25zdCB0ZXh0ID0gdGhpcy5mb2xsb3dCdXR0b24udGV4dCgpID09ICdGb2xsb3cnID8gJ0ZvbGxvd2luZycgOiAnRm9sbG93JztcbiAgICByZXR1cm4gdGhpcy5mb2xsb3dCdXR0b25cbiAgICAgIC50b2dnbGVDbGFzcygnYnRuLWluZm8nKVxuICAgICAgLnRvZ2dsZUNsYXNzKCdidG4tc2Vjb25kYXJ5JylcbiAgICAgIC50ZXh0KHRleHQpO1xuICB9XG5cbiAgZmFpbCgpIHtcbiAgICByZXR1cm4gYWxlcnQoJ1NvbWV0aGluZyBmYWlsZWQnKTtcbiAgfVxufVxuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vRXhoaWJpdERldGFpbHMvZXhoaWJpdERldGFpbHNDb250cm9sbGVyLmpzIl0sInNvdXJjZVJvb3QiOiIifQ==\n//# sourceURL=webpack-internal:///4\n");

/***/ })

},[139]);