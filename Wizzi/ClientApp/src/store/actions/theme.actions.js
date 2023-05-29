import { CAMBIAR_TEMA } from "../constants";

export const themeActionCreators = {
    changeTheme: paletteType => ({ type: CAMBIAR_TEMA, paletteType })
};