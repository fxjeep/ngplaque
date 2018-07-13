import { mainPanelActions } from '../Reducers/mainPanelReducer';
import request from '../Utils/request';

const contactsUrl = "http://localhost:4000/contacts";

export function getAllContacts() {
    return dispatch => {
        request("GET", contactsUrl)
            .then(function(contacts){
                contacts = contacts.map(x => ({
                    ...x,
                    key: x.id
                }))
                dispatch({ type: mainPanelActions.getAllContacts, contacts });
            });
    };
}

export function updateContact(contact){
    return dispatch => {
        request("POST", contactsUrl+"/"+contact.id, contact)
            .then(function(contacts){
                var i=0;
            });
    };
}