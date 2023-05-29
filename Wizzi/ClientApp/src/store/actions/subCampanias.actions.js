import {
    subCampaniasConstants
} from "../constants";
import { campaniasService } from '../../services';
import { notificationActions } from './';

export const subCampaniasActionCreators = {
    getSubcampaniaImg: codigoSubCampania => dispatch => {
        dispatch(request(codigoSubCampania));
        campaniasService.getSubcampaniaImg(codigoSubCampania)
            .then(
                img => {
                    dispatch(success(img));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(notificationActions.error(error));
                });

        function request(codigoSubCampania) { return { type: subCampaniasConstants.GET_IMG, codigoSubCampania } }
        function success(img) { return { type: subCampaniasConstants.GET_IMG_RECIBIR, img } }
        function failure(error) { return { type: subCampaniasConstants.GET_IMG_FALLIDO, error } }
    }
};