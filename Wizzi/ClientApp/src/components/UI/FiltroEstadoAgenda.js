import Chip from '@material-ui/core/Chip';
import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';
import React, { useEffect, useState } from 'react';
import { EstadoAgenda } from '../../enums';
import { obtenerDescripcionXestado, obtenerIconoEstadoAgenda } from '../../helpers';

export default function FiltroEstadoAgenda(props) {
  const { onChange, fixedOptions, selectedOptions } = props;
  const [options, setOptions] = useState([]);
  const [value, setValue] = useState([...fixedOptions]);

  useEffect(() => {
    const newOptions = Object.entries(EstadoAgenda).map(([key, value], i) => (
      value
    ));
    setOptions(newOptions);
  }, []);

  useEffect(() => {
    setValue([...fixedOptions, ...selectedOptions]);
  }, [fixedOptions, selectedOptions]);

  const onSelectChange = (event, newValues) => {
    const newValuesFiltered = [
      ...newValues.filter((option) => fixedOptions.indexOf(option) === -1),
    ]
    onChange(newValuesFiltered);
  }

  return (
    <Autocomplete
      multiple
      id="filtro-estado-agenda"
      value={value}
      onChange={onSelectChange}
      options={options}
      getOptionLabel={(option) => obtenerDescripcionXestado(option)}
      renderTags={(tagValue, getTagProps) =>
        tagValue.map((option, index) => (
          <Chip
            icon={obtenerIconoEstadoAgenda(option)}
            label={obtenerDescripcionXestado(option)}
            {...getTagProps({ index })}
            disabled={fixedOptions.indexOf(option) !== -1}
          />
        ))
      }
      renderInput={(params) => (
        <TextField
          {...params}
          label="Estado de agenda"
          variant="outlined"
          placeholder="Filtrar"
          size="small"
        />
      )}
      fullWidth
    />
  );
}