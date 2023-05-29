import {
    subCampaniasConstants
} from "../constants";
const initialState = { codigoSubCampania: '', img: '', isLoading: false };

export const subCampania = (state, action) => {
    state = state || initialState;
    switch (action.type) {

        case subCampaniasConstants.GET_IMG:
            return {
                codigoSubCampania: action.codigoSubCampania,
                img: '',
                isLoading: true
            };

        case subCampaniasConstants.GET_IMG_RECIBIR:
            return {
                ...state,
                img: action.img,
                isLoading: false
            };

        default:
            return state;
    }
};