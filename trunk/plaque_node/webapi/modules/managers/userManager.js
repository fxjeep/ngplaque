const UserModel = require('../models/user').UserModel;
const BaseError = require('../errors/error');

class UserManager{
    static checkUserLogin(username, password, done) {
        UserModel.findOne({email: username}, function (err, user) {
            if (err) {
                return done(err);
            }
            if (!user) {
                return done(new BaseError("User not found", 404), false);
            }
            if (!user.validate(password)) {
                return done(new BaseError("Invalid credentials", 500), false);
            }
            return done(null, user);
        });
    }
}

exports = module.exports = UserManager;
