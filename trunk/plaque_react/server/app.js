// 1. Include Packages
var express = require("express");
var bodyParser = require('body-parser');
var mongoose = require("mongoose");
const passport = require('passport');
var cors = require("cors");
var logger = require('morgan');

// 2. Include Configuration
var config = require('./config/config');

// 3. Initialize the application 
var app = express();
app.use(cors());
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// 4. Force https in production
// if (app.get('env') === 'production') {
//   app.use(function(req, res, next) {
//     var protocol = req.get('x-forwarded-proto');
//     protocol == 'https' ? next() : res.redirect('https://' + req.hostname + req.url);
//   });
// }

mongoose.connect(config.MONGO_URI, { useNewUrlParser: true });
mongoose.connection.on('error', function(err) {
  console.log('Error: Could not connect to MongoDB.');
});

require('./routes/routes')(app);

app.use('/', express.static('../plaque/build'))


app.use(passport.initialize());

app.listen(config.LISTEN_PORT, function(){
    console.log('listening on port ' + config.LISTEN_PORT);
});

module.exports = app;