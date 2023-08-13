import { createTheme } from '@mui/material/styles';

const globalTheme = createTheme({
  palette: {
    primary: {
      light: '#ccdbdc',
      main: '#80ced7',
      dark: '#003249',
      contrastText: '#242423',
    },
    secondary: {
      light: '#ccdbdc',
      main: '#003249',
      dark: '#f5cb5c',
      contrastText: '#242423',
    },
  },
  typography: {
    fontFamily: ['Roboto Mono', 'monospace'].join(','),
  },
});

export default globalTheme;
