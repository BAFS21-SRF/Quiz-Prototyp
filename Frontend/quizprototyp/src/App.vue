<template>
  <div id="app">
    <div class="scoreboard">
      <ScoreBoard :guid="guid"/>
    </div>
    <div class="numberplayers">
      <NumberPlayers/>
    </div>
    <div class="currentfrage">
      <CurrentFrage :guid="guid"/>
    </div>
  </div>
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
    const response = await axios.get('http://localhost:8888/api/gamestart');
    this.guid = response.data.guid;
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(2, 1fr);
  grid-column-gap: 0px;
  grid-row-gap: 0px;
}
.scoreboard { grid-area: 1 / 1 / 2 / 2; }
.numberplayers { grid-area: 1 / 2 / 2 / 3; }
.currentfrage { grid-area: 1 / 3 / 2 / 4; }
</style>
