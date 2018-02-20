'use strict';
 
var test = require('tape');
var request = require('supertest');
 
var app = require('../app.js');

test('POST /auth', function (assert) {

    var newThing = { email: "fxjeep@gmail.com", password: 'link1234'};

    request(app)
      .post('/auth')
      .send(newThing)
      .expect(200)
      .expect('Content-Type', /json/)
      .end(function (err, res) {
        assert.error(err, 'No error');
        assert.end();
      });
  });

  exports = module.exports = app;