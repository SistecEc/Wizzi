import { Toolbar } from '@devexpress/dx-react-scheduler-material-ui';
import { FormControl, InputLabel, MenuItem, Select, withStyles } from '@material-ui/core';
import React, { PureComponent } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { checkEmptyObject } from '../../../helpers/utils';
import { notificationActions } from "../../../store/actions";

const FlexSpaceStyles = theme => (
    {
        root: {
            display: 'flex',
            flex: 1,
            flexDirection: 'row',
            justifyContent: 'space-around',
        },
        selectorFonoaudiologo: {
            width: 200,
            textTransform: 'uppercase',
        },
    }
);

const initialState = {
    ciudades: [],
    cargandoCiudades: false,
}

class FlexibleSpaceComponent extends PureComponent{
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {

    }

    componentDidUpdate() {
    }

    render() {
        const { classes, recursosEmpleados, empleadoFiltrar, onEmpleadoSeleccionado, ...restProps } = this.props;
        const empleadosCargados = !checkEmptyObject(recursosEmpleados);

        return (
            <Toolbar.FlexibleSpace {...restProps} className={classes.root}>
                <FormControl>
                    <InputLabel id="lblEmpleado">
                        Fonoaudiólogo
                </InputLabel>
                    <Select
                        labelId="lblEmpleado"
                        id="cmbEmpleado"
                        name="empleadoFiltrar"
                        value={empleadoFiltrar}
                        onChange={onEmpleadoSeleccionado}
                        className={classes.selectorFonoaudiologo}
                    >
                        <MenuItem value={0}>TODOS</MenuItem>
                        {
                            empleadosCargados ?
                                recursosEmpleados.instances.map(empleado => {
                                    return (
                                        <MenuItem key={empleado.id} value={empleado.id}>{empleado.text}</MenuItem>
                                    )
                                })
                                :
                                null
                        }
                    </Select>
                </FormControl>
            </Toolbar.FlexibleSpace>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(FlexSpaceStyles)(FlexibleSpaceComponent));
