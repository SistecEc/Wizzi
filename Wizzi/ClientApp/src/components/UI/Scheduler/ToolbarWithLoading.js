import React from 'react';
import { Toolbar } from '@devexpress/dx-react-scheduler-material-ui';
import { makeStyles } from '@material-ui/core';
import LinearProgress from '@material-ui/core/LinearProgress';

const toolbarStyles = makeStyles(theme => ({
    toolbarRoot: {
        position: 'relative',
    },
    progress: {
        position: 'absolute',
        width: '100%',
        bottom: 0,
        left: 0,
    },
}));

export const ToolbarWithLoading = ({ children, ...restProps }) => {
    const classes = toolbarStyles();
    return (
        <div className={classes.toolbarRoot}>
            <Toolbar.Root {...restProps}>
                {children}
            </Toolbar.Root>
            <LinearProgress className={classes.progress} />
        </div>
    );
}
