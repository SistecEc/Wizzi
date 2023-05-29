import React from 'react';
import { MonthView } from '@devexpress/dx-react-scheduler-material-ui';
import { withStyles } from '@material-ui/core';
import { customCellStyles } from './Styles';
import { seDebeResaltarFecha } from './Utils';

const CustomMonthDayCellComponentBase = ({ classes, onDoubleClick, customOnDoubleClick, startDate, endDate, today, fechaResaltar, ...restProps }) => {

    const internalDblClick = (e) => {
        if (customOnDoubleClick) {
            customOnDoubleClick(e, { startDate, endDate, today });
        } else {
            onDoubleClick(e);
        }
    }

    return <MonthView.TimeTableCell
        {...restProps}
        onDoubleClick={internalDblClick}
        startDate={startDate}
        endDate={endDate}
        today={today}
        className={seDebeResaltarFecha(fechaResaltar, startDate, endDate) ? classes.celdaResaltada : null}
    />
}

export const CustomMonthDayCellComponent = withStyles(customCellStyles, { name: 'resaltada' })(CustomMonthDayCellComponentBase);