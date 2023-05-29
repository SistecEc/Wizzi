import React from 'react';
import { WeekView } from '@devexpress/dx-react-scheduler-material-ui';
import { withStyles } from '@material-ui/core';
import { customCellStyles } from './Styles';
import { seDebeResaltarFecha } from './Utils';

const CustomWeekTimeTableCellComponentBase = ({ classes, onDoubleClick, ValidadorContinuarDblClick, startDate, endDate, fechaResaltar, ...restProps }) => {

    const internalDblClick = (e) => {
        if (ValidadorContinuarDblClick({ startDate, endDate })) {
            onDoubleClick(e);
        }
    }

    return <WeekView.TimeTableCell
        {...restProps}
        onDoubleClick={internalDblClick}
        startDate={startDate}
        endDate={endDate}
        className={seDebeResaltarFecha(fechaResaltar, startDate, endDate) ? classes.celdaResaltada : null}
    />
}

export const CustomWeekTimeTableCellComponent = withStyles(customCellStyles, { name: 'resaltada' })(CustomWeekTimeTableCellComponentBase);