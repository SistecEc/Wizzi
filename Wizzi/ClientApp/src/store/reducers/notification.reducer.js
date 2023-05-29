import { notificationConstants } from '../constants';

const initialState = {
    visible: false,
    severity: '',
    message: ''
};

export const notification = (state, action) => {
    state = state || initialState;
    switch (action.type) {
        case notificationConstants.SUCCESS:
            return {
                visible: true,
                severity: 'success',
                message: action.message
            };

        case notificationConstants.ERROR:
            return {
                visible: true,
                severity: 'error',
                message: action.message
            };

        case notificationConstants.ALERT:
            return {
                visible: true,
                severity: 'warning',
                message: action.message
            };

        case notificationConstants.REMOVE:
            //return state.filter(notification => notification.id !== action.id);
            return {};

        case notificationConstants.CLEAR:
            return {};

        default:
            return state
    }
};