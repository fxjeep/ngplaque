const mongoose = require('mongoose');


let AncestorSchema = mongoose.Schema({
    contactId : {type:String, trim:true},
    surname:  {type:String, unique: true, trim: true},
    name: {type:String, uppercase:true, trim: true, index: true, unique: true},
    lastPrint: {type:String}
});

module.exports = mongoose.model('Ancestor', AncestorSchema);
