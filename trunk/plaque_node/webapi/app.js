var path = require('path'),
    express = require('express'),
    http = require('http'),
    csrf = require('csurf'),
    bodyParser = require('body-parser'),
    cookieParser = require('cookie-parser'),
    mongoose = require('mongoose'),
    compression = require('compression');

var authCtrl = require('./modules/controllers/authcontroller')
global.config = require('./configure');


var app = express();

// Switch off the default 'X-Powered-By: Express' header
app.disable('x-powered-by');

// compress all requests
app.use(compression());

/* Makes connection asynchronously.  Mongoose will queue up database
operations and release them when the connection is complete. */
mongoose.Promise = global.Promise;
mongoose.connect(global.config.MONGO_CONNECT_URL, function (err, res) {
  if (err) {
  console.log ('ERROR connecting to: ' + global.config.MONGO_CONNECT_URL + '. ' + err);
  } else {
  console.log ('Succeeded connected to: ' + global.config.MONGO_CONNECT_URL);
  }
});


app.use(bodyParser.urlencoded({extended: false}));
app.use(cookieParser());
app.use(csrf());

// error handler for csrf tokens
app.use(function (err, req, res, next) {
    if (err.code !== 'EBADCSRFTOKEN') {
        return next(err);
    }
    // handle CSRF token errors here
    res.status(403);
    res.send('Session has expired or form tampered with.');
})

app.use(express.static(path.join(__dirname, 'public')));

//setup routes
var router = express.Router();
router.use('/auth', authCtrl);


// Configure passport
var server = http.createServer(app).listen(global.config.PORT);

module.exports = server;