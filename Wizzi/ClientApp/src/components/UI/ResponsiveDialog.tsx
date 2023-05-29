import Dialog, { DialogProps } from '@material-ui/core/Dialog';
import { useTheme } from '@material-ui/core/styles';
import { Breakpoint } from '@material-ui/core/styles/createBreakpoints';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import React from 'react';

export interface ResponsiveDialogProps extends DialogProps {
    breakpoint?: Breakpoint
}

export const ResponsiveDialog = ({ breakpoint, children, ...restProps }: ResponsiveDialogProps) => {
    const theme = useTheme();
    const fullScreen = useMediaQuery(theme.breakpoints.down(breakpoint ? breakpoint : 'sm'));

    return (
        <Dialog
            {...restProps}
            fullScreen={fullScreen}
        >
            { children}
        </Dialog>
    );
}