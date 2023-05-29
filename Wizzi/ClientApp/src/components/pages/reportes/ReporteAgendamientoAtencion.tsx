import React, { Fragment, useEffect, useState } from 'react';
import { reportesService  } from '../../../services/reportes.services';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import {
    Button,
    Grid, 
    Card,
    Typography,
    Container
} from '@material-ui/core';
import { Loader } from '../../UI';
// import { DataGrid } from '@material-ui/data-grid';
import TextField from '@material-ui/core/TextField';
import CmbSucursales from '../../UI/CmbSucursales';
import CmbEmpleados from '../../UI/CmbEmpleados';
import SearchIcon from '@material-ui/icons/Search';
import dayjs from 'dayjs';
import GraficoBarra from '../solicitudesCitas/GraficoBarra';

const ReporteAgendamientoAtencion = () => {

    const [ciudadFiltrar, setCiudadFiltrar] = useState('');
    const [sucursalFiltrar, setSucursalFiltrar] = useState('');
    const [empleadoFiltrar, setempleadoFiltrar] = useState('');
    const [result, setresult] = useState({cantidadLEAD:0, data:[]});
    var date = new Date();
    const [fechainicio, setfechainicio] = useState(dayjs(date.setDate(date.getDate() - 5)).format("YYYY-MM-DD").toString());
    const [fechafinal, setfechafinal] = useState(dayjs(new Date()).format("YYYY-MM-DD").toString())
    const [citaTipo, setcitaTipo] = React.useState('0');
    const [estadoSpinner, setestadoSpinner] = React.useState(false);
    // const { height, widthpantalla } = useWindowDimensions();

  const handleChange = (event:any) => {
      console.log(event.target.value)
    setcitaTipo(event.target.value);
  };
  
    const typografyStyly={ 
        fontSize: 28,
        paddingTop: 30
    }

    const cargarSolicitudes =  (sucursal:string, empleado:string,fechaInicio:string, fechaFinal:string, estadoagenda="1") => {
        setestadoSpinner(true)
        const params: any = { sucursal,empleado,fechaInicio, fechaFinal,estadoagenda  };
         reportesService.getSolicitudes(params)
            .then(
                (resultados: any) => {
                    
                    setresult(JSON.parse(JSON.stringify(resultados)))
                    getDataCountLEADS()
                    setestadoSpinner(false)
                },
                error => {
                    console.log(error)
                })
            .finally(() => {
            });
    }

    const consultarprueba =async ()=>{
        var idempresa = " ";
        var idsucursal = " ";
        setresult({cantidadLEAD:0, data:[]})
        if (sucursalFiltrar.length !== 0){
            idsucursal = sucursalFiltrar;
        }
        if (empleadoFiltrar.length !== 0){
            idempresa = empleadoFiltrar;
        }
        await cargarSolicitudes(idsucursal,idempresa,fechainicio,fechafinal,citaTipo)
    }

    const columns = [
        { field: "motivoCita", headerName: "Motivo", width: 380, sortable: false, sortoptions: false },  //hide: true   
        { field: "observaciones", headerName: "Observaciones", width: 340,sortable: false  },
        { field: "tipoFinalizacion", headerName: "Tipo Fin.", width: 230,sortable: false  },
        { field: "categoriaFinalizacion", headerName: "Categoria Fin.", width: 120,sortable: false  },
        { field: "campania", headerName: "campañia", width: 120,sortable: false  }
      ];

      const getDataCountLEADS = () =>{
            console.log(result)
            const countAgendados = result.data.filter((x:any) => x.agendado =="SI").length;
            const countAtendidos = result.data.filter((x:any) => x.atendido =="SI").length;
            const countCandidatos = result.data.filter((x:any) => x.candidato =="SI").length;
            const countVendidos = result.data.filter((x:any) => x.vendido =="SI").length;
            // const countLeads = result.filter((x:any) => x.campania !==null).length;
            
            return [result.cantidadLEAD,countAgendados,countAtendidos,countCandidatos,countVendidos]
      }
    return (
        <>
        <Card >
            <Typography  align="center" style={typografyStyly}>
                REPORTE DE AGENDAMIENTO Y ATENCION
            </Typography>
            
            <Grid container spacing={2} style={{marginTop:'15px', paddingLeft:'10px', paddingRight:'10px'}}> 
                <Grid item md={12} sm={12}  >
                    <Container style={{textAlign:'center',display:'flex', justifyContent:'center'}}>
                        <GraficoBarra datos={getDataCountLEADS()} labels={['LEADS','Agendado','Atendido','Candidato','Vendido']}  />
                    </Container>
                </Grid>
                <Grid item xs={6}>
                    <CmbSucursales
                            incluirItemTodas={true}
                            valorInicialSeleccionado={sucursalFiltrar}
                            onValorSeleccionado={(e:any) => setSucursalFiltrar(e.target.value)}
                            soloParaAgendar={true}
                            formControlProps={{
                                variant: "outlined",
                                fullWidth: true,
                            }}
                        />
                </Grid>
                <Grid item xs={6} lg={6}>
                    <CmbEmpleados
                            incluirItemTodas={true}
                            valorInicialSeleccionado={empleadoFiltrar}
                            ciudadFiltrar={ciudadFiltrar}
                            sucursalFiltrar={sucursalFiltrar}
                            soloPuedeAgendar={true}
                            onValorSeleccionado={(e:any) => setempleadoFiltrar(e.target.value)}
                            formControlProps={{
                                variant: "outlined",
                                fullWidth: true
                            }}
                        />
                </Grid> 
            </Grid>
            <Grid container spacing={2} style={{marginTop:'15px', paddingLeft:'10px', paddingRight:'10px'}}>
                <Grid item md={6} xs={6}>
                        <TextField
                            id="date"
                            label="Ficha Inicio"
                            type="date"
                            defaultValue={fechainicio}
                            InputLabelProps={{
                            shrink: true
                            }}
                            
                            value={fechainicio}
                            onChange={(e) => setfechainicio(e.target.value)}
                            style={{width:'100%'}}
                        />
                </Grid>
                <Grid item md={6} xs={6}>
                        <TextField
                            id="date"
                            label="Ficha Final"
                            type="date"
                            defaultValue={fechafinal}
                            InputLabelProps={{
                            shrink: true,
                            }}
                            value={fechafinal}
                            onChange={(e) => setfechafinal(e.target.value)}
                            style={{width:'100%', textAlign:'center'}}
                        />
                </Grid>
            </Grid>
            <br/>
            <Grid container justify="center" style={{paddingBottom:'10px'}}>
                <Grid item md={6} xs={6} style={{width:'100%'}}>
                    <FormControl variant="outlined"  style={{width:'90%', marginLeft:'10px'}}>
                        <InputLabel id="demo-simple-select-outlined-label">Citas</InputLabel>
                        <Select
                        labelId="demo-simple-select-outlined-label"
                        id="demo-simple-select-outlined"
                        style={{width:'100%'}}
                        label="Tipo Agendamiento"
                        onChange={handleChange}
                        value={citaTipo}
                        >
                        <MenuItem value={0}>LEADS (SOLICITUD)</MenuItem>
                        <MenuItem value={1}>AGENDADOS</MenuItem>
                        <MenuItem value={2}>ATENDIDOS</MenuItem>
                        <MenuItem value={3}>CANDIDATOS</MenuItem>
                        <MenuItem value={4}>VENDIDOS</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item md={6} xs={6} style={{textAlign:'center'}}>
                <Button  
                            variant="contained"
                            onClick={async (e) => await consultarprueba()}
                            style={{ width:'80%',
                                    marginTop:'10px', 
                                    textAlign:'center'}}
                            startIcon={<SearchIcon />}
                            >
                        <Typography >
                            Consultar
                        </Typography>
                    </Button>
                </Grid>
            </Grid>
        </Card>
        <br></br>
        <Card>
            <br/>
            <Typography style={{display:'none'}}>
                    NUMERO DE LEAD: {result.cantidadLEAD}
            </Typography>
            {
                estadoSpinner ? <Loader/> : ""
            }
            <br/>
            <div > 
                {result.data.length > 0 ? (
                    <table  style={{borderCollapse:'collapse', width:'100%'}}>
                        <thead>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Fecha</th>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Nombre</th>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Centro</th>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Campaña</th>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Agendado</th>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Atendido</th>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Candidato</th>
                            <th style={{paddingTop: '12px', 
                                        paddingBottom:'12px', 
                                        textAlign:'center',
                                        border:'1px solid #ddd'}}>Vendido</th>
                        </thead>
                        <tbody>
                            {result.data.map( (item:any) =>(
                                <tr>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{dayjs(item.fecha).format("YYYY-MM-DD").toString()}</td>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{item.nombre}</td>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{item.centro}</td>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{item.campania}</td>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{item.agendado}</td>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{item.atendido}</td>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{item.candidato}</td>
                                <td style={{border:'1px solid #ddd', padding:'8px'}}>{item.vendido}</td>
                              </tr>
                            ))}
                        </tbody>
                    </table>
                                // <DataGrid  
                                // disableColumnFilter={false}
                                // disableExtendRowFullWidth={true}
                                // autoHeight={true}
                                // style={{width: '100%'}}
                                // rows={result}
                                // columns={columns}
                                // components={{
                                //     Footer:  () => <div style={{ display:'none', }}>hey a footer</div>,
                                // }}
                                // disableColumnSelector={true}
                                // />
                ):<Typography style={{textAlign:'center'}}>
                           NO EXISTEN DATOS PARA MOSTRAR
                 </Typography> }
            </div>
        </Card>
    </>
    )
}

export default ReporteAgendamientoAtencion
