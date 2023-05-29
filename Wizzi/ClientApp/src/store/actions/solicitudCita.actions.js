import {
    solicitudCitaConstants
} from "../constants";
import { citasService } from '../../services';
import { notificationActions } from './';

export const solicitudCitaActionCreators = {
    grabarSolicitudCita: infoSolicitud => dispatch => {
        dispatch(request(infoSolicitud));
        citasService.grabarSolicitudCita(infoSolicitud)
            .then(
                infoSolicitudGrabada => {
                    dispatch(success(infoSolicitudGrabada));
                    dispatch(notificationActions.success('Se ha registrado su solicitud de cita correctamente.'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(notificationActions.error(error));
                });

        function request(infoSolicitud) { return { type: solicitudCitaConstants.GRABAR_REQUEST, infoSolicitud } }
        function success(infoSolicitud) { return { type: solicitudCitaConstants.GRABAR_CORRECTO, infoSolicitud } }
        function failure(error) { return { type: solicitudCitaConstants.GRABAR_FALLIDO, error } }
    },
    obtenerSolicitudesCitas: () => dispatch => {
        dispatch(request());
        citasService.obtenerSolicitudesCitas()
            .then(
                infoSolicitudes => {
                    dispatch(success(infoSolicitudes));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(notificationActions.error(error));
                });

        function request() { return { type: solicitudCitaConstants.OBTENER_TODAS_REQUEST } }
        function success(infoSolicitudes) { return { type: solicitudCitaConstants.OBTENER_TODAS_CORRECTO, infoSolicitudes } }
        function failure(error) { return { type: solicitudCitaConstants.OBTENER_TODAS_FALLIDO, error } }
    },
    obtenerSolicitudCita: id => dispatch => {
        dispatch(request(id));
        citasService.obtenerSolicitudCita(id)
            .then(
                infoSolicitud => {
                    dispatch(success(infoSolicitud));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(notificationActions.error(error));
                });

        function request(id) { return { type: solicitudCitaConstants.OBTENER_REQUEST, id } }
        function success(infoSolicitud) { return { type: solicitudCitaConstants.OBTENER_CORRECTO, infoSolicitud } }
        function failure(error) { return { type: solicitudCitaConstants.OBTENER_FALLIDO, error } }
    }
};