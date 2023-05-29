import {
    REQUEST_CITAS_MEDICAS,
    RECEIVE_CITAS_MEDICAS
} from "../constants";

export const agendaActionCreators = {
  requestCitasMedicas: startDateIndex => (dispatch, getState) => {
    //   if (startDateIndex === getState().CitasMedicas.startDateIndex) {
    //     // Don't issue a duplicate request (we already have or are loading the requested data)
    //     return;
    //   }

    dispatch({ type: REQUEST_CITAS_MEDICAS, startDateIndex });
    return fetch(`api/agendas/CitasMedicas?startDateIndex=${startDateIndex}`)
        .then(response => response.json())
        .then(citasMedicas => {
            dispatch({ type: RECEIVE_CITAS_MEDICAS, startDateIndex, citasMedicas });
        });
  }
};