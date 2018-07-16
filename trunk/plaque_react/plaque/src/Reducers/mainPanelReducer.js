export const mainPanelActions = {
    getAllContacts:"GetAllContacts",
    deleteContact:"DeleteContact",
    addContact:"AddContact"
};

const initialState = {
    contacts:[]
};

export function mainPanel(state = initialState, action) {
    switch (action.type) {
        case mainPanelActions.getAllContacts:
            return {
                contacts: action.contacts,
            };
        case mainPanelActions.deleteContact:
            var lst = state.contacts.filter(contact => contact.id !==action.id);
            return { contacts: lst};
        case mainPanelActions.addContact:
            return { ...state, contacts: state.contacts.concat(action.newcontact)};            
        default:
            return state
    }
  }