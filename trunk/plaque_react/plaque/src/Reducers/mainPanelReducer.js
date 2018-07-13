export const mainPanelActions = {
    getAllContacts:"getAllContacts"
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
      default:
        return state
    }
  }