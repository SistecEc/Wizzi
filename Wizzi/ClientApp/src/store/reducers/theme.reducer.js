//import useMediaQuery from '@material-ui/core/useMediaQuery';
import { getCookie } from '../../helpers/cookieManager'
import {
    CAMBIAR_TEMA
} from "../constants";

//const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)');

const cookie = getCookie('paletteType') || null;
const initialState = { paletteType: cookie ? cookie : 'light' };

export const theme = (state, action) => {
    state = state || initialState;

    if (action.type === CAMBIAR_TEMA) {
        return {
            paletteType: action.paletteType
        };
    }

    return state;
};