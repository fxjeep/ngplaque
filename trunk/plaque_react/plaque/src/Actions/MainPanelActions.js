import { mainPanelActions } from '../Reducers/mainPanelReducer';
import request from '../Utils/request';

const contactsUrl = "http://localhost:4000/contacts";

export function getAllContacts() {
    return dispatch => {
        request("GET", contactsUrl)
            .then(function(response){
                var contacts = response.data.contacts.map(x => ({
                    ...x,
                    key: x.id
                }))
                dispatch({ type: mainPanelActions.getAllContacts, contacts });
            });
    };
}

export function updateContact(contact){
    return dispatch => {
        let cloned = { ... contact };
        delete cloned.id;
        request("PUT", contactsUrl+"/"+contact.id, cloned)
            .then(function(contacts){
                var i=0;
            });
    };
}

export function deleteContact(id){
    return dispatch => {
        request("DELETE", contactsUrl+"/"+id)
            .then(function(contacts){
                dispatch({ type: mainPanelActions.deleteContact, id });
            });
    };
}

export function addNewContact(){
    return dispatch => {
        let newcontact = {
            "name": "test",
            "code":"z99999",
            "lastPrint":"2000-01-01",
        };
        request("POST", contactsUrl, newcontact)
            .then(function(newcontact){
                dispatch({ type: mainPanelActions.addContact, newcontact });
            });
        
    };
}

