import { notificationConstants } from '../constants';

export const notificationActions = {
    success,
    error,
    alert,
    remove,
    clear
};

function success(message) {
    return { type: notificationConstants.SUCCESS, message };
}

function error(message) {
    return { type: notificationConstants.ERROR, message };
}

function alert(message) {
    return { type: notificationConstants.ALERT, message };
}

function remove(id) {
    return { type: notificationConstants.REMOVE, id };
}

function clear() {
    return { type: notificationConstants.CLEAR };
}