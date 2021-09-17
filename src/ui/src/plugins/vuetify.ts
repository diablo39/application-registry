import Vue from 'vue'
import Vuetify from 'vuetify/lib'

import '@/sass/overrides.sass'

Vue.use(Vuetify)

const theme = {
  primary: '#4682B4', /*'#4CAF50',*/
  secondary: '#9C27b0',
  accent: '#9C27b0',
  info: '#00CAE3',
}

const vuetify = new Vuetify({
  theme: {
    default: 'light',
    disable: false,
    themes: {
      light: theme
    },
  },
})

export default vuetify