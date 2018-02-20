var expect = require('chai').expect;
const UserManager = require('../modules/managers/userManager'); 
var  mongoose = require('mongoose');
global.config = require('../configure');

mongoose.Promise = global.Promise;
mongoose.connect(global.config.MONGO_CONNECT_URL, function (err, res) {
  if (err) {
  console.log ('ERROR connecting to: ' + global.config.MONGO_CONNECT_URL + '. ' + err);
  } else {
  console.log ('Succeeded connected to: ' + global.config.MONGO_CONNECT_URL);
  }
});
 
describe('TestUser', function() {
    it('should pass, login as fxjeep', function(done) {
        UserManager.checkUserLogin("fxjeep@gmail.com", "link1234", function(error, user){
            done();
        });
    });
});

