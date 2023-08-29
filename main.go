package main

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

type Exercise struct {
	ID          string `json:"id"`
	Title       string `json:"title"`
	Description string `json:"description"`
	PictureUrl  string `json:"pictureUrl"`
}

type StepType string

const (
	Start    StepType = "start"
	Read     StepType = "read"
	Practice StepType = "practice"
)

type Step struct {
	ID          string     `json:"id"`
	Title       string     `json:"title"`
	Description string     `json:"description"`
	Type        StepType   `json:"type"`
	Exercises   []Exercise `json:"exercises"`
}

var Be = []Step{{
	ID:          "0",
	Title:       "Перад тым як пачаць",
	Description: "Дзеля далейшага адсочвання прагрэсу, выканай, калі ласка, чатыры папярэдніх малюнка. На адваротным баку кожнага падпішы дату і свае адчуванні адносна намаляванага: што падабаецца, што не, як і што хацелася бы змяніць і г.д. Гэтыя малюнкі спатрэбяцца табе пазней, каб бачыць як ты расцеш.",
	Type:        Start,
	Exercises: []Exercise{{
		ID:          "0-0",
		Title:       "Малюнак чалавека якога бачыш",
		Description: "Першы малюнак - любы чалавек, якога бачыш перад сабой. Фотаздымак не падыдзе, трэба маляваць з натуры. Чалавек цалкам, ці партрэт - не важна, як табе падабаецца. І без перфекцыянізма - гэтыя малюнкі - кропка адліку, а не твой першы шэдэўр.",
		PictureUrl:  "/src/assets/man1.png",
	}, {
		ID:          "0-1",
		Title:       "Малюнак чалавека з галавы",
		Description: "Цяпер уяві і намалюй чалавека. Дэталі, фон і атрыбуты - не важны.",
		PictureUrl:  "/src/assets/man2.png",
	}, {
		ID:          "0-2",
		Title:       "Малюнак тваёй рукі",
		Description: "Наступны малюнак - твая рука. Адной малюеш, а на другую глядзіш і малюеш яе.",
		PictureUrl:  "/src/assets/hand.png",
	}, {
		ID:          "0-3",
		Title:       "Крэсла якое бачыш",
		Description: "І нарэшце - крэсла якое ты бачыш, не на фотаздымку, а перад сабой.",
		PictureUrl:  "/src/assets/chair.png",
	},
	}}, {
	ID:          "1",
	Title:       "Левае і правае паўшар'і мозга",
	Description: "JТут будзе апісанне, але крышачку пазней.",
	Type:        Read,
	Exercises:   make([]Exercise, 0, 0)}, {
	ID:          "2",
	Title:       "Вазы-твары ці вазы Рубіна",
	Description: "Апісанне...",
	Type:        Practice,
	Exercises: []Exercise{{
		ID:          "2-0",
		Title:       "Твар чалавека ў профіль",
		Description: "Апісанне...",
		PictureUrl:  "/src/assets/rubin1.png",
	}, {
		ID:          "2-1",
		Title:       "Твар пачвары ў профіль",
		Description: "Апісанне...",
		PictureUrl:  "/src/assets/rubin2.png",
	},
	}},
}

var Ru = []Step{{
	ID:          "0",
	Title:       "Перед тем как начать",
	Description: "Выполни, пожалуйста, предварительные рисунки, на обратной стороне каждого поставь дату и опиши свои ощущения касательно нарисованного, что нравится, а что нет. Эти рисунки пригодятся тебе в дальнейшем для отслеживания прогресса.",
	Type:        Start,
	Exercises: []Exercise{{
		ID:          "0-0",
		Title:       "Рисунок человека которого видишь",
		Description: "Первый рисунок - любой человек, которого видишь в живую. Фотография не подойдёт, рисуй с натуры. Человек целиком, или же портрет - не важно, как тебе нравится. И без перфекционизма - эти рисунки - точка отсчёта, а не твой первый шедевр.",
		PictureUrl:  "/src/assets/man1.png",
	}, {
		ID:          "0-1",
		Title:       "Рисунок человека из головы",
		Description: "Теперь вообрази и нарисуй человека. Не вдавайся в сильные детали, фон и атрибуты - не важны.",
		PictureUrl:  "/src/assets/man2.png",
	}, {
		ID:          "0-2",
		Title:       "Рисунок твоей руки",
		Description: "Следующий рисунок - твоя рука, левши рисуют правую, правши - левую.",
		PictureUrl:  "/src/assets/hand.png",
	}, {
		ID:          "0-3",
		Title:       "Стул который видишь",
		Description: "Выбери какой-нибудь стул, лучше обычный, но можно и вычурный, не на фотографии, а в живую, и нарисуй.",
		PictureUrl:  "/src/assets/chair.png",
	},
	}}, {
	ID:          "1",
	Title:       "Левое и правое полушария мозга",
	Description: "Тут будет описание, но чуточку позже.",
	Type:        Read,
	Exercises:   make([]Exercise, 0, 0)}, {
	ID:          "2",
	Title:       "Вазы-лица или вазы Рубина",
	Description: "Описание...",
	Type:        Practice,
	Exercises: []Exercise{{
		ID:          "2-0",
		Title:       "Лицо человека в профиль",
		Description: "Описание...",
		PictureUrl:  "/src/assets/rubin1.png",
	}, {
		ID:          "2-1",
		Title:       "Лицо чудовища в профиль",
		Description: "Описание...",
		PictureUrl:  "/src/assets/rubin2.png",
	},
	}},
}

func main() {
	router := gin.Default()
	router.GET("/:locale/steps", getSteps)

	router.Run("localhost:8080")
}

func getSteps(c *gin.Context) {
	locale := c.Param("locale")
	var steps []Step
	switch locale {
	case "be":
		steps = Be
	case "ru":
		steps = Ru
	default:
		steps = Be
	}
	c.IndentedJSON(http.StatusOK, steps)
}
