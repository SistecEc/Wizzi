import React from 'react';
import { IconButton } from '@material-ui/core';
import { getIcon } from './Utils';

export const CommandButtonComponent = ({ id, onExecute, mostrarVisualizar, ...restProps }) => {
    return <IconButton
        {...restProps}
        onClick={onExecute}
    >
        {getIcon(id, mostrarVisualizar)}
    </IconButton>;
};
