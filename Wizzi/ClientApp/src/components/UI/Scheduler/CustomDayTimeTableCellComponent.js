import React from 'react';
import { DayView } from '@devexpress/dx-react-scheduler-material-ui';
import { withStyles } from '@material-ui/core';
import { customCellStyles } from './Styles';
import { seDebeResaltarFecha } from './Utils';

const CustomDayTimeTableCellComponentBase = ({ classes, onDoubleClick, ValidadorContinuarDblClick, startDate, endDate, fechaResaltar, ...restProps }) => {

    const internalDblClick = (e) => {
        if (ValidadorContinuarDblClick({ startDate, endDate })) {
            onDoubleClick(e);
        }
    }

    return <DayView.TimeTableCell
        {...restProps}
        onDoubleClick={internalDblClick}
        startDate={startDate}
        endDate={endDate}
        className={seDebeResaltarFecha(fechaResaltar, startDate, endDate) ? classes.celdaResaltada : null}
    />
}

export const CustomDayTimeTableCellComponent = withStyles(customCellStyles, { name: 'resaltada' })(CustomDayTimeTableCellComponentBase);