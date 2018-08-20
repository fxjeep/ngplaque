const authsvc = require('../services/authsvc');
var config = require('../config');
var user = require('../models/user');
var mongoose = require("mongoose");

mongoose.connect(config.MONGO_URI, { useNewUrlParser: true });
mongoose.connection.on('error', function(err) {
  console.log('Error: Could not connect to MongoDB.');
});

var newuser = {
    name:"john2",
    password: "1234",
    group: "chinese",
    email: "john@eee.com"
};

        user.create(newuser)
        .then(function(user){
            if (user !=null){
                done();
            }
        })
        .catch(function(error){
            console.log(error);
        });

        authsvc.createUser(newuser)
                  .then(function(user){
                      if (user !=null){
                          done();
                      }
                  })
                  .catch(function(error){
                      console.log(error);
                  });


// var config = require('./config');
// var mongoose = require('mongoose');
// mongoose.connect(config.MONGO_URI);

// var user = mongoose.model('User', {
//     name: String,
//     password: String,
//     email: String
//   });

// var kitty = new user({ name: 'kitty', password:'123', email:'email@email.com'});
  

// kitty.save(function (err) {
//     if (err) 
//         console.log(err);
    
//     user.find({}, function(error, list){
//         console.log(list);
//     });
// });