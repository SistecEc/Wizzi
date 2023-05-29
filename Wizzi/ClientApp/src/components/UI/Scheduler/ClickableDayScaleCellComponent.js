import { WeekView } from '@devexpress/dx-react-scheduler-material-ui';
import { makeStyles } from '@material-ui/core';
import React from 'react';

const DayScaleClasses = makeStyles(theme => {
    return {
        root: {
            cursor: 'pointer',
            '&:hover': {
                boxShadow: theme.shadows[2],
            },
        },
    }
});

export const ClickableDayScaleCellComponent = ({ onClick, startDate, endDate, today, ...restProps }) => {
    const classes = DayScaleClasses();

    const internalClick = () => {
        onClick({ startDate, endDate, today });
    }

    return <WeekView.DayScaleCell
        {...restProps}
        className={classes.root}
        onClick={internalClick}
        startDate={startDate}
        endDate={endDate}
        today={today}
    />
}
