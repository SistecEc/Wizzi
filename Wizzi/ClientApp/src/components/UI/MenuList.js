import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Divider from "@material-ui/core/Divider";

import { Event, ViewAgenda, ViewList, ExitToApp } from "@material-ui/icons";

import List from "@material-ui/core/List";
import ListItemLink from "./ListItemLink";
import { ListSubheader } from "@material-ui/core";
import AssessmentIcon from '@material-ui/icons/Assessment';
import { ReactComponent as Logo } from './logo.svg';
import { ReactComponent as LogoDark } from './logoDark.svg';
import { connect } from "react-redux";

const useStyles = makeStyles(theme => ({
    toolbar: theme.mixins.toolbar,
    appIcon: {
        height: 100,
        maxWidth: '100%',
        padding: theme.spacing()
    }
}));

const menu = [
    {
        titulo: "Agendas",
        menus: [
            {
                icono: <Event />,
                descripcion: "Agenda",
                path: "/agenda"
            },
            {
                icono: <ViewAgenda />,
                descripcion: "Administrar citas",
                path: "/SolicitudesCitas"
            }
        ]
    },
    {
        titulo: "Campañas",
        menus: [
            {
                icono: <ViewList />,
                descripcion: "Campañas",
                path: "/Campanias"
            }
        ]
    },
    {
        titulo: "Reportes",
        menus: [
            {
                icono: <AssessmentIcon />,
                descripcion: "Reporte",
                path: "/ReporteAgendamientoAtencion"
            }
        ]
    },
    {
        titulo: "Cuenta",
        menus: [
            {
                icono: <ExitToApp />,
                descripcion: "Cerrar Sesión",
                path: "/Login"
            }
        ]
    },
];

function MenuList(props) {
    const classes = useStyles();
    return (
        <React.Fragment>
            {props.paletteType === 'light' ? 
                <Logo className={classes.appIcon}/>
            :
                <LogoDark className={classes.appIcon}/>
            }
            <div />
            {menu.map((menuGroup, i) => (
                <React.Fragment key={`s-${i}`}>
                    <Divider />
                    <List>
                        <ListSubheader>{menuGroup.titulo}</ListSubheader>
                        {menuGroup.menus.map((menuItem, j) => (
                            <ListItemLink
                                to={menuItem.path}
                                primary={menuItem.descripcion}
                                icon={menuItem.icono}
                                key={`m-${i}-${j}`}
                            />
                        ))}
                    </List>
                </React.Fragment>
            ))}
        </React.Fragment>
    );
}

export default connect(
    state => state.theme
)(MenuList);