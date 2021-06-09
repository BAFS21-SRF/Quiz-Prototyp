<template>
  <v-app>
    <v-app-bar
      app
      color="#2e2d29"
      dark
    >
      <div class="d-flex align-center">
        <v-img
          alt="SRF Logo"
          class="shrink mr-2"
          contain
          src="/images/SRF_Logo.png"
          transition="scale-transition"
          width="50"
        />
      </div>
    </v-app-bar>

    <v-main>
        <v-container>
          <v-row>
            <v-col><ScoreBoard/></v-col>
            <v-col><NumberPlayers/></v-col>
            <v-col><CurrentFrage :guid="guid"/></v-col>
          </v-row>
        </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import axios from 'axios';
import { Component, Vue } from 'vue-property-decorator';
import CurrentFrage from './components/CurrentFrage.vue';
import NumberPlayers from './components/NumberPlayers.vue';
import ScoreBoard from './components/ScoreBoard.vue';

@Component({
  components: {
    CurrentFrage,
    NumberPlayers,
    ScoreBoard,
  },
})
export default class App extends Vue {
  private guid: string = '';
  private async created() {
    const response = await axios.get('http://' + window.location.hostname + ':8888/api/gamestart');
    this.guid = response.data.guid;
  }
}
</script>
