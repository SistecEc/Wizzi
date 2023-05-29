import {
    campaniasConstants, campaniaConstants
} from "../constants";
import { campaniasService } from '../../services';
import { notificationActions } from './';

export const campaniasActionCreators = {
   getCampanias: () => dispatch => {
        dispatch(request());
        campaniasService.getCampanias()
            .then(
                campanias => {
                    dispatch(success(campanias));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(notificationActions.error(error));
                });

        function request() { return { type: campaniasConstants.GET } }
        function success(campanias) { return { type: campaniasConstants.GET_RECIBIR, campanias } }
        function failure(error) { return { type: campaniasConstants.GET_FALLIDO, error } }
    },
    getSubcampanias: codigoCampania => dispatch => {
        dispatch(request(codigoCampania));
        campaniasService.getSubcampanias(codigoCampania)
            .then(
                subcampanias => {
                    dispatch(success(subcampanias));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(notificationActions.error(error));
                });

        function request(codigoCampania) { return { type: campaniaConstants.GET_SUBCAMPANIAS, codigoCampania } }
        function success(subcampanias) { return { type: campaniaConstants.GET_SUBCAMPANIAS_RECIBIR, codigoCampania, subcampanias } }
        function failure(error) { return { type: campaniaConstants.GET_SUBCAMPANIAS_FALLIDO, error } }
    },
};