export const editDetailsActionsConst = {
    getDetails:"getDetails",
};

const initialState = {
    details:{
        'Live':[],
        'Deceased':[],
        'Ancestor':[]
    }
};

export function editDetails(state = initialState, action) {
    switch (action.type) {
        case editDetailsActionsConst.getDetails:
            return {
                details: action.details,
            };       
        default:
            return state
    }
  }