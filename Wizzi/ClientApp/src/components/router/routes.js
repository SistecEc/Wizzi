import { Route } from 'react-router-dom';

//UI
import { PrivateRoute, LayoutAuthenticated, LayoutAnonymous } from '../UI';

//PAGES
import { LoginPage } from '../pages/login';
import { RegisterPage } from '../pages/Register';
import solicitarCita from '../pages/solicitarCita/solicitarCita';
import campanias from '../pages/campania/campanias';
import ReporteAgendamientoAtencion from '../pages/reportes/ReporteAgendamientoAtencion';
import SolicitudesCitas from '../pages/solicitudesCitas/SolicitudesCitas';
import agenda from '../pages/agenda/agenda';

const routes = [
    {
        layout: LayoutAnonymous,
        routeComponent: Route,
        subRoutes: [
            {
                path: "/",
                component: LoginPage,
                exact: true
            },
            {
                path: "/login",
                component: LoginPage,
                exact: true
            },
            {
                path: "/register",
                component: RegisterPage,
                exact: true
            },
            {
                path: "/solicitarCita",
                component: solicitarCita,
                exact: true
            }
        ]
    },
    {
        layout: LayoutAuthenticated,
        routeComponent: PrivateRoute,
        subRoutes: [
            {
                path: "/SolicitudesCitas",
                component: SolicitudesCitas,
                exact: false
            },
            {
                path: "/Campanias",
                component: campanias,
                exact: false
            },
            {
                path: "/Agenda",
                component: agenda,
                exact: false
            }
            ,
            {
                path: "/ReporteAgendamientoAtencion",
                component: ReporteAgendamientoAtencion,
                exact: false
            }
        ]
    }
];

export default routes;