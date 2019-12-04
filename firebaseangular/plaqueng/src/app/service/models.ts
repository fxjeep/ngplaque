import {Utils} from './Utils';

export class Contact {
    static createNewContact(name:string, code:string):any{
        return {
            Name: name,
            Code : code,
            IsPrint: 0,
            FilterKey: name+"__"+code,
            LastPrint : ''
        };
    }
}