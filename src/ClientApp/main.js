import _ from 'lodash';
window.lodash = _;
window._ = _;

import $ from 'jquery';
window.jQuery = $;
window.$ = $;

import bootbox from 'bootbox';
window.bootbox = bootbox;

import moment from 'moment';
window.moment = moment;

import Rellax from 'rellax';
window.Rellax = Rellax;

import datepicker from 'bootstrap-datepicker';
window.datepicker= datepicker;

import 'bootstrap';
import 'ekko-lightbox';


import logoSmall from './images/logo.png';
var logoImg = document.getElementById('logo_sm');
logoImg.src = logoSmall;

import banner from './images/banner.jpg';
var bannerImg = document.getElementById('banner');

import './main.scss';

const globeContainer = document.getElementById('fa_globe');
globeContainer.innerHTML =
  '<i class="fa fa-globe" style="padding:8px; vertical-align: middle";></i>';

const Uppy = require('uppy/lib/core')
const Dashboard = require('uppy/lib/plugins/Dashboard')
const GoogleDrive = require('uppy/lib/plugins/GoogleDrive')
const Dropbox = require('uppy/lib/plugins/Dropbox')
const Instagram = require('uppy/lib/plugins/Instagram')
const Webcam = require('uppy/lib/plugins/Webcam')
const Tus = require('uppy/lib/plugins/Tus')
const uppy = Uppy({
  debug: true,
  autoProceed: false,
  restrictions: {
    maxFileSize: 1000000,
    maxNumberOfFiles: 3,
    minNumberOfFiles: 2,
    allowedFileTypes: ['image/*', 'video/*']
  }
})
.use(Dashboard, {
  trigger: '.UppyModalOpenerBtn',
  inline: true,
  target: '.DashboardContainer',
  replaceTargetContent: true,
  note: 'Images and video only, 2–3 files, up to 1 MB',
  maxHeight: 450,
  metaFields: [
    { id: 'license', name: 'License', placeholder: 'specify license' },
    { id: 'caption', name: 'Caption', placeholder: 'describe what the image is about' }
  ]
})
.use(GoogleDrive, { target: Dashboard, host: 'https://server.uppy.io' })
.use(Dropbox, { target: Dashboard, host: 'https://server.uppy.io' })
.use(Instagram, { target: Dashboard, host: 'https://server.uppy.io' })
.use(Webcam, { target: Dashboard })
.use(Tus, { endpoint: 'https://master.tus.io/files/' })
.run()
uppy.on('complete', result => {
  console.log('successful files:', result.successful)
  console.log('failed files:', result.failed)
})
