import React from "react";
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { themeActionCreators } from '../../store/actions'

import Brightness4Icon from '@material-ui/icons/Brightness4';
import Brightness7Icon from '@material-ui/icons/Brightness7';
import { IconButton } from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';

const ThemeToggler = (props) => {
    const { paletteType, changeTheme, className } = props;
    const cssclass = className || getDefaultStyle().defaultTogglerClass;

    const handleTogglePaletteType = () => {
        const nextPalette = paletteType === 'light' ? 'dark' : 'light';
        changeTheme(nextPalette);
    }

    return (
        <IconButton color="inherit" onClick={handleTogglePaletteType} className={cssclass}>
            {paletteType === 'light' ? <Brightness4Icon /> : <Brightness7Icon />}
        </IconButton>
    );
}

function getDefaultStyle() {
    const classes = makeStyles({
        defaultTogglerClass: {
            position: 'absolute',
            top: '0',
            right: '0',
        }
    })
    return classes();
}

const mapStateToProps = state => {
    return {
        paletteType: state.theme.paletteType
    };
};

const mapDispatchToProps = dispatch => {
    return bindActionCreators(themeActionCreators, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(ThemeToggler)