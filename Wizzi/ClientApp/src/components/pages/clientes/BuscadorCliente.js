import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import {
    withStyles,
    Button,
    Grid,
    TextField,
    TableContainer,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TablePagination,
    TableBody,
    IconButton,
    Box,
} from '@material-ui/core';
import SearchIcon from '@material-ui/icons/Search';

import { notificationActions } from '../../../store/actions';
import { clientesService } from '../../../services';
import { Skeleton } from '@material-ui/lab';

const initialState = {
    buscando: false,
    datoBuscar: '',
    pagina: 1,
    tamanioPagina: 10,
    resultadosBuscador: {}
};

const styles = theme => ({
    root: {
        width: '100%',
    },
    container: {
        maxHeight: 440,
    },
    circularInsideButton: {
        marginRight: theme.spacing(),
    },
});


const columnas = [
    {
        id: 'seleccionar',
        label: 'Seleccionar',
        align: 'center',
    },
    {
        id: 'identificacion',
        label: 'Identificación',
        style: {
            minWidth: 100,
        }
    },
    {
        id: 'nombre',
        label: 'Nombre',
        minWidth: 170,
        align: 'right',
    },
    {
        id: 'apellido',
        label: 'Apellido',
        style: {
            minWidth: 170,
        },
        align: 'right',
    },
    {
        id: 'nombreComercial',
        label: 'Nombre Comercial',
        style: {
            minWidth: 170,
        },
        align: 'right',
    },
];


class BuscadorCliente extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        if (this.props.buscarAlMontar) {
            this.cargarEstado(this.props.buscarAlMontar);
        }
    }

    componentDidUpdate(prevProps) {

    }

    cargarEstado(buscarAlMontar) {
        this.setState({
            datoBuscar: buscarAlMontar,
        }, this.buscarCliente);
    }

    handleChangetxt = (ev) => {
        const { name, value } = ev.target;
        this.setState({ [name]: value });
    }

    handleEntertxt = (ev) => {
        if (ev.key === 'Enter') {
            ev.preventDefault();
            this.buscarCliente();
        }
    }

    handleChangePage = (event, newPage) => {
        this.setState({ pagina: newPage }, this.buscarCliente());
    };

    handleChangeRowsPerPage = (event) => {
        this.setState({ pagina: 0, tamanioPagina: +event.target.value }, this.buscarCliente());
    };

    handleSeleccionarClick = (e) => {
        const { resultadosBuscador } = this.state;
        const datosCliente = resultadosBuscador.resultados.find(c => c.codigo === e.currentTarget.dataset.codigocliente);
        this.props.onClienteSeleccionado(datosCliente);
    }

    buscarCliente = () => {
        let { datoBuscar, pagina, tamanioPagina } = this.state;
        if (datoBuscar.trim() !== "") {
            this.setState({ buscando: true, pagina: 0 });
            clientesService.buscarClientes(datoBuscar, pagina, tamanioPagina)
                .then(
                    resultadosBuscador => {
                        this.setState({ resultadosBuscador });
                    },
                    error => {
                        this.props.notifications.error(error);
                    })
                .finally(() => {
                    this.setState({ buscando: false });
                });
        }
    }

    render() {
        const { classes } = this.props;
        const { buscando, datoBuscar, resultadosBuscador, tamanioPagina, pagina } = this.state;
        return (
            <Grid container spacing={2}>
                <Grid item xs={11}>
                    <TextField
                        variant="outlined"
                        id="txtDatoBuscar"
                        name="datoBuscar"
                        label="Buscar"
                        value={datoBuscar}
                        onChange={this.handleChangetxt}
                        onKeyDown={this.handleEntertxt}
                        autoFocus
                        fullWidth
                        required
                    />
                </Grid>
                <Grid container item xs={1} justify="center" alignItems="center">
                    <IconButton color="primary" aria-label="Buscar cliente" component="span" onClick={this.buscarCliente} disabled={buscando}>
                        <SearchIcon />
                    </IconButton>
                </Grid>
                <Grid item xs={12}>
                    {
                        <Box className={classes.root}>
                            <TableContainer className={classes.container}>
                                <Table stickyHeader aria-label="Buscador de cliente">
                                    <TableHead>
                                        <TableRow>
                                            {columnas.map((columna) => (
                                                <TableCell
                                                    key={columna.id}
                                                    align={columna.align}
                                                    style={columna.style}
                                                >
                                                    {
                                                        buscando ?
                                                            <Skeleton animation="wave" />
                                                            :
                                                            columna.label
                                                    }
                                                </TableCell>
                                            ))}
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {
                                            resultadosBuscador.resultados?.length > 0 ?
                                                resultadosBuscador.resultados.map((cliente, rowIndex) => {
                                                    return (
                                                        <TableRow hover key={rowIndex}>
                                                            {columnas.map((columna) => {
                                                                let contenidoCelda = null;
                                                                if (buscando) {
                                                                    contenidoCelda = <Skeleton animation="wave" />
                                                                } else {
                                                                    if (columna.id === "seleccionar") {
                                                                        contenidoCelda = (
                                                                            <Button
                                                                                variant="contained"
                                                                                color="secondary"
                                                                                type="submit"
                                                                                onClick={this.handleSeleccionarClick}
                                                                                data-codigocliente={cliente.codigo}
                                                                                size="small"
                                                                            >
                                                                                Seleccionar
                                                                            </Button>
                                                                        );
                                                                    } else {
                                                                        contenidoCelda = cliente[columna.id];
                                                                    }
                                                                }
                                                                return (
                                                                    <TableCell key={columna.id} align={columna.align}>
                                                                        {contenidoCelda}
                                                                    </TableCell>
                                                                );
                                                            })}
                                                        </TableRow>
                                                    );
                                                })
                                                :
                                                buscando ?
                                                    null
                                                    :
                                                    <TableRow hover>
                                                        <TableCell align="center" colSpan="5">
                                                            No hay resultados
                                                        </TableCell>
                                                    </TableRow>
                                        }
                                    </TableBody>
                                </Table>
                            </TableContainer>
                            {
                                buscando ?
                                    <Skeleton animation="wave" />
                                    :
                                    resultadosBuscador.resultados?
                                        <TablePagination
                                            rowsPerPageOptions={[10, 25, 100]}
                                            component="div"
                                            count={resultadosBuscador.totalRegistros}
                                            rowsPerPage={tamanioPagina}
                                            page={pagina}
                                            onChangePage={this.handleChangePage}
                                            onChangeRowsPerPage={this.handleChangeRowsPerPage}
                                        />
                                        :
                                        null
                            }
                        </Box>
                    }
                </Grid>
                <Grid container item justify="center">
                    <Button
                        variant="contained"
                        color="primary"
                        type="submit"
                        onClick={this.props.onRegistrarClick}
                    >
                        Registrar paciente
                    </Button>
                </Grid>
            </Grid>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(BuscadorCliente));
