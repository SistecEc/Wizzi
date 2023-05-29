import React, { useEffect } from "react";
import { connect } from 'react-redux';

import { MuiThemeProvider, createMuiTheme, responsiveFontSizes } from '@material-ui/core/styles';
import { lightGreen, green } from "@material-ui/core/colors";

const ThemeProvider = (props) => {
    const { paletteType } = props.theme;
    const { children } = props;

    const theme = responsiveFontSizes(
        createMuiTheme({
            palette: {
                primary: {
                    main: green[500]
                },
                secondary: {
                    main: lightGreen[500]
                },
                type: paletteType,
            },
            spacing: 8
        })
    );

    useEffect(() => {
        document.cookie = `paletteType=${paletteType};path=/;max-age=31536000`;
    }, [paletteType]);


    return (
        <MuiThemeProvider theme={theme}>
            {children}
        </MuiThemeProvider>
    );
}

const mapStateToProps = state => {
    return {
        theme: state.theme
    };
};

export default connect(mapStateToProps)(ThemeProvider)