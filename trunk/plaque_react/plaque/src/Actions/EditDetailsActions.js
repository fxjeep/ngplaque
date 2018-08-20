import { editDetailsActionsConst } from '../Reducers/editDetailsReducer';
import request from '../Utils/request';
import { history } from '../Utils/history';

export const editDetailActions = {
    getDetail
}

function getDetail(contactId){
    return dispatch => {
            var detail = {
            "Live":[
                {
                    name:"dddd",
                    lastPrint:"20180101"
                },
                {
                    name:"dddd",
                    lastPrint:"20180101"
                }
            ],
            "Deceased":[
                {
                    dname:"dddd",
                    lname:"deee",
                    relation:"12233",
                    lastPrint:"20180101"
                },
            ],
            "Ancestor":[
                {
                    surname:"dee",
                    name:"deee",
                    lastPrint:"20180101"
                }
            ]
        };
    dispatch({ type: editDetailsActionsConst.getDetails, detail });
    }
}