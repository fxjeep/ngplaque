const authsvc = require('../services/authsvc');
var config = require('../config');
var mongoose = require("mongoose");

mongoose.connect(config.MONGO_URI, { useNewUrlParser: true });
mongoose.connection.on('error', function(err) {
  console.log('Error: Could not connect to MongoDB.');
});

// process.on('unhandledRejection', error => {
//     // Will print "unhandledRejection err is not defined"
//     console.log('unhandledRejection', error.message);
//   });

describe('user model', function () {
    it('test1 should create user', function(done){
        var newuser = {
            name:"john2",
            password: "1234",
            group: "chinese",
            email: "john@eee.com"
        };
        var user = authsvc.createUser(newuser)
                        .then(function(user){
                            if (user !=null){
                                done();
                            }
                        })
                        .catch(function(error){
                            console.log(error);
                        });
    });
});