const { ExtractJwt, Strategy } = require('passport-jwt');
const  user = require('../models/user');
const config = require('../config');

module.exports = function(passport){
    var opts = {};
    opts.jwtFromRequest = ExtractJwt.fromAuthHeaderAsBearerToken();
    opts.secretOrKey = CONFIG.jwt_encryption;

    passport.use(new Strategy(opts, async function(jwt_payload, done){
        let err, user;
        [err, user] = await to(user.findById(jwt_payload.user_id));
        if(err) return done(err, false);
        if(user) {
            return done(null, user);
        }else{
            return done(null, false);
        }
    }));
}