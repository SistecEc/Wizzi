const reduxLogger = store => next => action => {
    next(action);
};

export default reduxLogger;