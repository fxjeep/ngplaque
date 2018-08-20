const mongoose 			= require('mongoose');


let ContactSchema = mongoose.Schema({
    name:  {type:String, unique: true, trim: true},
    code: {type:String, uppercase:true, trim: true, index: true, unique: true},
    lastPrint: {type:String}
});

module.exports = mongoose.model('Contact', ContactSchema);
