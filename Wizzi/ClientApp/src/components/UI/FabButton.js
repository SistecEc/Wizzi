import React from "react";
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';


export const FabButton = (props) => {
    return (
        <Fab color="primary" aria-label="add" {...props}>
            <AddIcon />
        </Fab>
    );
}