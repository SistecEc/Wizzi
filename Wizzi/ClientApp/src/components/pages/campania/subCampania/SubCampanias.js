import React, { Component } from 'react';
import SubCampania from './SubCampania';

import {
    Grid,
} from '@material-ui/core';
import { AgregarSubCampania } from './AgregarSubCampania';

class SubCampanias extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
    }

    renderSubCampanias = (subCampanias) => {
        let buffer = [];
        buffer.push(<AgregarSubCampania key={0} onClick={this.props.onAgregarClick}/>);

        if (subCampanias) {
            buffer = buffer.concat(subCampanias.map((subCampania, i) => (
                <SubCampania infoSubcampania={subCampania} key={i + 1} onEditar={this.props.onEditarClick} onDelete={this.props.onDeleteClick}/>
            )));
        }

        return buffer
    }

    render() {
        return (
            <Grid container spacing={2} justify="center">
                {this.renderSubCampanias(this.props.infoSubCampanias)}
            </Grid>
        );
    }
}

export default SubCampanias;
