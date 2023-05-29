import { Grid, Typography } from '@material-ui/core';
import CircularProgress from '@material-ui/core/CircularProgress';
import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';
import * as dayjs from 'dayjs';
import React, { Fragment, useEffect, useState } from 'react';
import { agendasService } from '../../services';

function sleep(delay = 0) {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
}

export default function BuscadorCitaPaciente(props) {
  const { onChange } = props;
  const [open, setOpen] = useState(false);
  const [agendas, setAgendas] = useState([]);
  const [inputValue, setInputValue] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    let active = true;
    const timeOutId = setTimeout(() => {
      if (inputValue != "") {
        setLoading(true);

        (async () => {
          agendasService.getBuscarAgendas({ q: inputValue })
            .then(
              agendas => {
                if (active) {
                  setAgendas(agendas);
                }
              },
              error => {
                // this.props.notifications.error(error);
              })
            .finally(() => {
              setLoading(false);
            });
        })();

      }
    }, 300);
    return () => {
      active = false;
      clearTimeout(timeOutId)
    };
  }, [inputValue]);

  useEffect(() => {
    if (!open) {
      setAgendas([]);
    }
  }, [open]);

  const inputChange = (event, newInputValue) => {
    setInputValue(newInputValue);
  }

  const opcionSeleccionada = (event, value, reason) => {
    onChange(event, value, reason);
  }

  return (
    <Autocomplete
      id="Buscar-Agenda-demo"
      open={open}
      onOpen={() => {
        setOpen(true);
      }}
      onClose={() => {
        setOpen(false);
      }}
      getOptionLabel={(agenda) => {
        const { cita } = agenda;
        const { cliente } = cita;
        return `${cliente.apellido} ${cliente.nombre}`
      }}
      options={agendas}
      loading={loading}
      loadingText="Cargando..."
      fullWidth={true}
      onInputChange={inputChange}
      onChange={opcionSeleccionada}
      filterOptions={(agendas) => agendas}
      renderInput={(params) => (
        <TextField
          {...params}
          label="Buscar paciente"
          variant="outlined"
          size="small"
          InputProps={{
            ...params.InputProps,
            endAdornment: (
              <Fragment>
                {loading ? <CircularProgress color="inherit" size={20} /> : null}
                {params.InputProps.endAdornment}
              </Fragment>
            ),
          }}
        />
      )}
      renderOption={(agenda) => {
        const { cita, empleado } = agenda;
        const { cliente } = cita;
        return (
          <Grid container direction="column">
            <Grid item container direction="row" alignContent="center" justify="space-between">
              <Grid item>
                <Typography noWrap>
                  {
                    `${cliente.apellido} ${cliente.nombre}`
                  }
                </Typography>
              </Grid>
              <Grid item>
                <Typography noWrap variant="caption">
                  {
                    `${dayjs(agenda.fechaInicio).format("DD MMM. YYYY HH:mm:ss")}`
                  }
                </Typography>
              </Grid>

            </Grid>
            <Grid item container direction="row" alignContent="center" justify="space-between">
              <Grid item>
                <Typography noWrap variant="caption">
                  {
                    agenda.titulo
                  }
                </Typography>
              </Grid>
              <Grid item>
                <Typography noWrap variant="caption">
                  {
                    `${empleado.apellido} ${empleado.nombre}`
                  }
                </Typography>
              </Grid>
            </Grid>
          </Grid>
        )
      }
      }
    />
  );
}
